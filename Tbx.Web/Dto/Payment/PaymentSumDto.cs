using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TbxPortal.Web.Dto.Payment
{
    public class PaymentSumDto
    {
        public double Sum { get; set; }

        public string FormattedSum
        {
            get
            {
                return String.Format("{0:N}", Sum);
            }
        }
    }
}