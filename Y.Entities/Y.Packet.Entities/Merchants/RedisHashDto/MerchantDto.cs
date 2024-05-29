using System;
using static Y.Packet.Entities.Merchants.Merchant;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Packet.Entities.Merchants.RedisHashDto
{
    public class MerchantDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public decimal GameCredit { get; set; }

        public string Status { get; set; }

        public DateTime CreateDate { get; set; }

        public string PageSectionConfig { get; set; }

        public string VipsConfig { get; set; }

        public string Domains { get; set; }

        public string IpWhitelist { get; set; }


        public string PcTempletStr { get; set; }

        public string H5TempletStr { get; set; }

        public string SignupConfig { get; set; }

        public string CustomerConfig { get; set; }


        public Merchant ToEntity()
        {
            return new Merchant
            {
                Id = Id,
                Name = Name,
                GameCredit = GameCredit,
                Status = Status.ToEnum<MerStatus>().Value,
                CreateDate = CreateDate,
                PageSectionConfig = PageSectionConfig,
                VipsConfig = VipsConfig,
                Domains = Domains,
                IpWhitelist = IpWhitelist,
                PcTempletStr = PcTempletStr,
                H5TempletStr = H5TempletStr,
                SignupConfig = SignupConfig,
                CustomerConfig = CustomerConfig
            };
        }
    }
}
