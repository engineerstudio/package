using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2021-04-02 18:50:04
    /// 
    /// </summary>
    public partial class GameLogsSlot
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String GameTypeStr { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 UserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string PlayerName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string SourceId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal BetAmount { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal ValidBet { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Money { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal AwardAmount { get; set; }

        /// <summary>
        /// Ͷע��ʼʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime SourceOrderCreateTime { get; set; }

        /// <summary>
        /// �ɽ�����ʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime SourceOrderAwardTime { get; set; }

        /// <summary>
        ///  Ͷע��ʼʱ�� utc+8
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime OrderCreateTimeUtc8 { get; set; }

        /// <summary>
        ///  �ɽ�����ʱ�� utc+8
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime OrderAwardTimeUtc8 { get; set; }

        /// <summary>
        ///  ƽ̨ץȡʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime CreateTimeUtc8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime SettlementTimeUtc8 { get; set; }

        /// <summary>
        /// ��Ϸ����
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String SlotName { get; set; }

        /// <summary>
        /// Ͷע�豸
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String Device { get; set; }

        /// <summary>
        /// IP��ַ
        /// </summary>
        [Required]
        [MaxLength(20)]
        public String Ip { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        public String Raw { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        [Required]
        [MaxLength(3)]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// ����ǰ���
        /// </summary>
        [Required]
        [MaxLength(19)]
        public decimal CreditBefore { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [Required]
        [MaxLength(19)]
        public decimal CreditAfter { get; set; }

    }
}
