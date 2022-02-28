using CashpointWPF.DB.Entities;

namespace CashpointWPF.Model.Mappers
{
    public class ClientMapper : IMapper<Client, ClientDTO>
    {
        public Client ToEntity(ClientDTO model)
        {
            return new Client
            {
                Id = model.Id,
                Name = model.Name,
                Account = new Account
                {
                    Id = model.Account.Id,
                    Balance = model.Account.Balance
                }
            };
        }

        public ClientDTO ToModel(Client entity)
        {
            return new ClientDTO(
                entity.Name,
                new AccountDTO(
                    entity.Account.Balance,
                    entity.Account.Id),
                entity.Id);
        }
    }
}
