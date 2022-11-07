using Microsoft.AspNetCore.Authorization;

namespace GastroFaza.Authorization.Policy
{
    public class MinimumAgeRequirment : IAuthorizationRequirement
    {
        public int MinAge { get;}

        public MinimumAgeRequirment(int minAge)
        {
            MinAge = minAge;
        }
    }
}
