namespace PsmjCo.NerdStore.Vendas.Application.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Communication.Mediator;
    using Core.DomainObjects.DTO;
    using Core.Extensions;
    using Core.Messages;
    using Core.Messages.CommonMessages.IntegrationEvents;
    using Core.Messages.CommonMessages.Notifications;
    using Domain;
    using Events;
    using MediatR;

    public class PedidoCommandHandler :
        IRequestHandler<AdicionarItemPedidoCommand, bool>,
        IRequestHandler<RemoverItemPedidoCommand, bool>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>,
        IRequestHandler<AplicarVoucherPedidoCommand, bool>,
        IRequestHandler<IniciarPedidoCommand, bool>,
        IRequestHandler<FinalizarPedidoCommand, bool>,
        IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>,
        IRequestHandler<CancelarProcessamentoPedidoCommand, bool>
    {
        private readonly IMediatorHandler mediatorHandler;
        private readonly IPedidoRepository pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler)
        {
            this.pedidoRepository = pedidoRepository;
            this.mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!this.ValidarComando(message)) return false;

            var pedido = await this.pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
                pedido.AdicionarItem(pedidoItem);

                this.pedidoRepository.Adicionar(pedido);
                pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(message.ClienteId, message.ProdutoId));
            }
            else
            {
                var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExistente)
                    this.pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                else
                    this.pedidoRepository.AdicionarItem(pedidoItem);

                pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            }

            pedido.AdicionarEvento(
                new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));
            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!this.ValidarComando(message)) return false;

            var pedido = await this.pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var pedidoItem = await this.pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if (pedidoItem != null && !pedido.PedidoItemExistente(pedidoItem))
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return false;
            }

            pedido.RemoverItem(pedidoItem);
            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(message.ClienteId, pedido.Id, message.ProdutoId));

            this.pedidoRepository.RemoverItem(pedidoItem);
            this.pedidoRepository.Atualizar(pedido);

            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!this.ValidarComando(message)) return false;

            var pedido = await this.pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var pedidoItem = await this.pedidoRepository.ObterItemPorPedido(pedido.Id, message.ProdutoId);

            if (!pedido.PedidoItemExistente(pedidoItem))
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return false;
            }

            pedido.AtualizarUnidades(pedidoItem, message.Quantidade);

            pedido.AdicionarEvento(new PedidoProdutoAtualizadoEvent(message.ClienteId, pedido.Id, message.ProdutoId, message.Quantidade));
            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));

            this.pedidoRepository.AtualizarItem(pedidoItem);
            this.pedidoRepository.Atualizar(pedido);

            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!this.ValidarComando(message)) return false;

            var pedido = await this.pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            if (pedido == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var voucher = await this.pedidoRepository.ObterVoucherPorCodigo(message.CodigoVoucher);

            if (voucher == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Voucher não encontrado!"));
                return false;
            }

            var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);
            if (!voucherAplicacaoValidation.IsValid)
            {
                foreach (var error in voucherAplicacaoValidation.Errors)
                    await this.mediatorHandler.PublicarNotificacao(new DomainNotification(error.ErrorCode, error.ErrorMessage));

                return false;
            }

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(message.ClienteId, pedido.Id, voucher.Id));

            this.pedidoRepository.Atualizar(pedido);

            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!this.ValidarComando(message)) return false;

            var pedido = await this.pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);
            pedido.IniciarPedido();

            var itensList = new List<Item>();
            pedido.PedidoItems.ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            pedido.AdicionarEvento(
                new PedidoIniciadoEvent(
                    pedido.Id,
                    pedido.ClienteId,
                    listaProdutosPedido,
                    pedido.ValorTotal,
                    message.NomeCartao,
                    message.NumeroCartao,
                    message.ExpiracaoCartao,
                    message.CvvCartao));

            this.pedidoRepository.Atualizar(pedido);
            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinalizarPedidoCommand message, CancellationToken cancellationToken)
        {
            var pedido = await this.pedidoRepository.ObterPorId(message.PedidoId);

            if (pedido == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            pedido.FinalizarPedido();

            pedido.AdicionarEvento(new PedidoFinalizadoEvent(message.PedidoId));
            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelarProcessamentoPedidoEstornarEstoqueCommand message, CancellationToken cancellationToken)
        {
            var pedido = await this.pedidoRepository.ObterPorId(message.PedidoId);

            if (pedido == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var itensList = new List<Item>();
            pedido.PedidoItems.ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            pedido.AdicionarEvento(new PedidoProcessamentoCanceladoEvent(pedido.Id, pedido.ClienteId, listaProdutosPedido));
            pedido.TornarRascunho();

            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelarProcessamentoPedidoCommand message, CancellationToken cancellationToken)
        {
            var pedido = await this.pedidoRepository.ObterPorId(message.PedidoId);

            if (pedido == null)
            {
                await this.mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            pedido.TornarRascunho();

            return await this.pedidoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
                this.mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));

            return false;
        }
    }
}