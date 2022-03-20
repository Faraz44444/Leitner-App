using System;

namespace TagPortal.Domain.Model.Payment
{
    public class PaymentSumModel
    {
        public float Sum { get; set; }

        public string FormattedSum
        {
            get
            {
                return String.Format("{0:N}", Sum);
            }
        }
    }
}
