using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Core.Request.BaseRequests
{
    public interface IBaseRequest
    {
        long CreatedByUserId { get; set; }
        string CreatedByFullName { get; set; }
        DateTime? CreatedFrom { get; set; }
        DateTime? CreatedTo { get; set; }
        bool? Deleted { get; set; }
        DateTime? DeletedFrom { get; set; }
        DateTime? DeletedTo { get; set; }
    }

    public abstract class BaseRequest : IBaseRequest
    {
        public virtual long ClientId { get; set; }
        public virtual long CreatedByUserId { get; set; }
        public virtual string CreatedByFullName { get; set; }
        public virtual DateTime? CreatedFrom { get; set; }
        public virtual DateTime? CreatedTo { get; set; }
        public virtual bool? Deleted { get; set; }
        public virtual DateTime? DeletedFrom { get; set; }
        public virtual DateTime? DeletedTo { get; set; }
        public EnumOrderByDirection OrderByDirection { get; set; }
        public string OrderBy { get; set; }

        public bool Validate()
        {
            return CreatedByUserId > 0;
        }
        public virtual string BaseWhereSql(string tableAlias, bool ignoreClientId = false)
        {
            var searchParams = new List<string>();
            if (ClientId > 0 && !ignoreClientId) searchParams.Add($"{tableAlias}.ClientId = @ClientId");
            if (Deleted.HasValue) searchParams.Add($"{tableAlias}.Deleted = @Deleted");
            if (DeletedFrom.HasValue) searchParams.Add($"{tableAlias}.DeletedAt >= @DeletedAtFrom");
            if (DeletedTo.HasValue) searchParams.Add($"{tableAlias}.DeletedAt <= @DeletedAtTo");

            if (CreatedByUserId > 0) searchParams.Add($"{tableAlias}.CreatedByUserId = @CreatedByUserId");
            if (!CreatedByFullName.Empty()) searchParams.Add($"REPLACE(CONCAT({tableAlias}.CreatedByFirstName, {tableAlias}.CreatedByLastName), ' ', '') like '%' + REPLACE(@CreatedByFullName, ' ', '') + '%'");

            if (CreatedFrom.HasValue) searchParams.Add($"{tableAlias}.CreatedAt >= @CreatedAtFrom");
            if (CreatedTo.HasValue)
            {
                CreatedTo = CreatedTo.EndOfDay();
                searchParams.Add($"{tableAlias}.CreatedAt <= @CreatedAtTo");
            }
            return $" {string.Join(" AND ", searchParams)}";
        }
    }
}
