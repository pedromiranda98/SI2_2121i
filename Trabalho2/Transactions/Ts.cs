using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Transaction
{
    public class Ts
    {
        public static TransactionScope GetTsReadCommitted()
        {
            var option = new TransactionOptions();
            option.IsolationLevel = IsolationLevel.ReadCommitted;
            option.Timeout = TimeSpan.FromMinutes(5);

            return new TransactionScope(TransactionScopeOption.Required, option);
        }

        public static TransactionScope GetTsSerializable()
        {
            var option = new TransactionOptions();
            option.IsolationLevel = IsolationLevel.Serializable;
            option.Timeout = TimeSpan.FromMinutes(5);

            return new TransactionScope(TransactionScopeOption.Required, option);
        }

        public static TransactionScope GetTsReadUnCommitted()
        {
            var option = new TransactionOptions();
            option.IsolationLevel = IsolationLevel.ReadUncommitted;
            option.Timeout = TimeSpan.FromMinutes(5);

            return new TransactionScope(TransactionScopeOption.Required, option);
        }
    }
}
