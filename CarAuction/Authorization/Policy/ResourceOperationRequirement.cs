using Microsoft.AspNetCore.Authorization;

namespace CarAuction.Authorization.Policy
{
    public enum ResourcOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {

        public ResourceOperationRequirement(ResourcOperation resourcOperation)
        {
            ResourcOperation = resourcOperation;
        }


        public ResourcOperation ResourcOperation { get; }

    }
}
