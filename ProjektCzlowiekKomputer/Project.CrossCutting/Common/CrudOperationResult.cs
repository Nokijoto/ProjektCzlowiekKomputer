using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Common
{
    public abstract class CrudOperationResult<TDto>
    {
        public CrudOperationResultStatus Status { get; set; }

        public TDto? Result { get; set; }
    }
}
