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
	public partial class GameLogsLottery
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

        /// <summary>
        /// ��Ϸ���
        /// </summary>
        [Required]
        [MaxLength(3)]
        public Byte GameCategory { get; set; }

        /// <summary>
        /// ��Ϸ
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String GameTypeStr { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// �û�ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 UserId { get; set; }

        /// <summary>
        /// ��Ϸ�û�����
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PlayerName { get; set; }

        /// <summary>
        /// ��ԴId
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String SourceId { get; set; }

        /// <summary>
        /// Ͷע����ID
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String MatchName { get; set; }

        /// <summary>
        /// Ͷע��
        /// </summary>
        [Required]
        [MaxLength(50)]
        public String BetItem { get; set; }

        /// <summary>
        /// Ͷע����
        /// </summary>
        [Required]
        [MaxLength(50)]
        public String BetContent { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [Required]
        [MaxLength(50)]
        public String Results { get; set; }

        /// <summary>
        /// Ͷע���
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal BetAmount { get; set; }

        /// <summary>
        /// ��ЧͶע
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal ValidBet { get; set; }

        /// <summary>
        /// ӯ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Money { get; set; }

        /// <summary>
        /// �ɽ����
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal AwardAmount { get; set; }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime SourceOrderCreateTime { get; set; }

        /// <summary>
        /// �����ɽ�ʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime SourceOrderAwardTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime OrderCreateTimeUtc8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime OrderAwardTimeUtc8 { get; set; }

        /// <summary>
        /// ����״̬
        /// </summary>
        [Required]
        [MaxLength(3)]
        public Byte Status { get; set; }

        /// <summary>
        /// 1.���� 2.���� 3.����
        /// </summary>
        [Required]
        [MaxLength(3)]
        public Stage Stage { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal SourceOdds { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(3)]
        public Byte SourceOddsType { get; set; }

        /// <summary>
        /// ŷ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal DEOdds { get; set; }

        /// <summary>
        /// ������ƽ̨����ʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ����������ʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// ����ƽ̨����ʱ��
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime SettlementTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        public String Raw { get; set; }


    }
}
