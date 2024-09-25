using KitaraKauppa.Service.Shared_Dtos;

namespace KitaraKauppa.Service.UsersService.Dtos
{
    public class UsersQueryOptions : QueryOptions<UserOrderOptions>
    {
        public Guid? UserRoleId { get; set; }

        public new UsersOrderWith OrderUserWith { get; set; } = UsersOrderWith.LastName;

    }
}
