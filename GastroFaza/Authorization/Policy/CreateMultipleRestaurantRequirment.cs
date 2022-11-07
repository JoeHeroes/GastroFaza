using Microsoft.AspNetCore.Authorization;

namespace GastroFaza.Authorization.Policy
{
    public class CreateMultipleRestaurantRequirment : IAuthorizationRequirement
    {
        public CreateMultipleRestaurantRequirment(int _MinimumCreatedUni)
        {
            MinimumCreatedUni = _MinimumCreatedUni;
        }
        public int MinimumCreatedUni {get;}
    }
}
