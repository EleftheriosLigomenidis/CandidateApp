using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business.Utilities
{
    public class Messages
    {
        public const string UserMessage = "usermessage";
        public static string FetchCollection(string entityName) =>
               $"Fetching collection of {entityName}s from memory/database.";

        public static string FetchEntity(string entityName, string identifier, string id) =>
            $"Fetching {entityName} from memory/database with {identifier} {id}.";

        public static string CreatingEntity(string entityName, string entity) =>
            $"Creating {entityName} {entity}.";

        public static string UpdatingEntity(string entityName, string entity) =>
            $"Updating {entityName} {entity}.";

        public static string DeletingEntity(string entityName, string id) =>
            $"Deleting {entityName} with id {id}.";

        public static string FetchCollectionFailed(string entityName) =>
            $"Fetching collection of {entityName}s failed.";

        public static string EntityReferenced(string entityName,string id, string parentEntity) =>
            $"Entity {entityName} with id {id} is referenced in table of {parentEntity}";
        public static string FetchEntityFailed(string entityName, string id) =>
            $"Fetching {entityName} from memory/database with id {id}.";

        public static string CreateEntityFailed(string entityName, string entity) =>
            $"Creating entity {entityName} {entity} failed.";

        public static string UpdateEntityFailed(string entityName, string id) =>
            $"Updating entity {entityName} with id {id} failed.";

        public static string DeleteEntityFailed(string entityName, string id) =>
            $"Deleting entity {entityName} with id {id} failed.";

        public static string EntityNotFound(string entityName, string id) =>
            $"The entity {entityName} with id {id} was not found.";

    }
}
