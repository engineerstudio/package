using System.Threading.Tasks;
using Y.Infrastructure.IApplication;
using Y.Packet.Repositories.IGames;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Repositories.IPay;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Repositories.IVips;

namespace Y.Infrastructure.Application
{
    public class PlatformDataInitializationCacheService : IPlatformDataInitializationCacheService
    {


        private readonly IDomiansRepository domiansRepository;
        private readonly IMerchantRepository merchantRepository;
        private readonly ISectionDetailRepository sectionDetailRepository;
        private readonly ISectionKeyRepository sectionKeyRepository;

        private readonly IVipGroupsRepository vipGroupsRepository;


        private readonly IPayCategoryRepository payCategoryRepository;
        private readonly IPayMerchantRepository payMerchantRepository;
        private readonly IWithdrawMerchantRepository withdrawMerchantRepository;

        private readonly IPromotionsConfigRepository promotionsConfigRepository;
        private readonly IPromotionsTagRepository promotionsTagRepository;


        private readonly IGameApiTimestampsRepository gameApiTimestampsRepository;
        private readonly IGameInfoRepository gameInfoRepository;
        private readonly IGamelogsMd5CacheRepository gamelogsMd5CacheRepository;
        private readonly IGameMerchantRepository gameMerchantRepository;
        private readonly IGameUsersRepository gameUsersRepository;
        private readonly IGameLogsLotteryRepository gameLogsLotteryRepository;

        private readonly IUserHierarchyRepository userHierarchyRepository;
        private readonly IUsersBankRepository usersBankRepository;
        private readonly IUsersFundsRepository usersFundsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IUsersSessionRepository usersSessionRepository;

        public PlatformDataInitializationCacheService(IDomiansRepository domiansRepository, IMerchantRepository merchantRepository, ISectionDetailRepository sectionDetailRepository, ISectionKeyRepository sectionKeyRepository, IVipGroupsRepository vipGroupsRepository, IPayCategoryRepository payCategoryRepository, IWithdrawMerchantRepository withdrawMerchantRepository, IPayMerchantRepository payMerchantRepository, IPromotionsConfigRepository promotionsConfigRepository, IPromotionsTagRepository promotionsTagRepository, IGameApiTimestampsRepository gameApiTimestampsRepository, IGameInfoRepository gameInfoRepository, IGamelogsMd5CacheRepository gamelogsMd5CacheRepository, IGameMerchantRepository gameMerchantRepository, IGameUsersRepository gameUsersRepository, IUserHierarchyRepository userHierarchyRepository, IUsersBankRepository usersBankRepository, IUsersFundsRepository usersFundsRepository, IUsersRepository usersRepository, IUsersSessionRepository usersSessionRepository, IGameLogsLotteryRepository gameLogsLotteryRepository)
        {
            this.domiansRepository = domiansRepository;
            this.merchantRepository = merchantRepository;
            this.sectionDetailRepository = sectionDetailRepository;
            this.sectionKeyRepository = sectionKeyRepository;
            this.vipGroupsRepository = vipGroupsRepository;
            this.payCategoryRepository = payCategoryRepository;
            this.payMerchantRepository = payMerchantRepository;
            this.withdrawMerchantRepository = withdrawMerchantRepository;
            this.promotionsConfigRepository = promotionsConfigRepository;
            this.promotionsTagRepository = promotionsTagRepository;
            this.gameApiTimestampsRepository = gameApiTimestampsRepository;
            this.gameInfoRepository = gameInfoRepository;
            this.gamelogsMd5CacheRepository = gamelogsMd5CacheRepository;
            this.gameMerchantRepository = gameMerchantRepository;
            this.gameUsersRepository = gameUsersRepository;
            this.userHierarchyRepository = userHierarchyRepository;
            this.usersBankRepository = usersBankRepository;
            this.usersFundsRepository = usersFundsRepository;
            this.usersRepository = usersRepository;
            this.usersSessionRepository = usersSessionRepository;
            this.gameLogsLotteryRepository = gameLogsLotteryRepository;
        }


        /// <summary>
        /// 迁移数据库的数据到Redis
        /// 后续改进 需要执行方法加上属性 MigrateSqlDbToRedisDb, 反射程序执行初始化
        /// </summary>
        /// <returns></returns>
        public async Task MigrateSqlDbToRedisDbAsync()
        {

            // 1. 站点
            await domiansRepository.MigrateSqlDbToRedisDbAsync();
            await merchantRepository.MigrateSqlDbToRedisDbAsync();
            await sectionDetailRepository.MigrateSqlDbToRedisDbAsync();
            await sectionKeyRepository.MigrateSqlDbToRedisDbAsync();

            // 2. VIps
            await vipGroupsRepository.MigrateSqlDbToRedisDbAsync();

            // 3. Pay
            await payCategoryRepository.MigrateSqlDbToRedisDbAsync();
            await payMerchantRepository.MigrateSqlDbToRedisDbAsync();
            await withdrawMerchantRepository.MigrateSqlDbToRedisDbAsync();

            // 4. Promotion
            await promotionsConfigRepository.MigrateSqlDbToRedisDbAsync();
            await promotionsTagRepository.MigrateSqlDbToRedisDbAsync();

            // 5. Games
            await gameApiTimestampsRepository.MigrateSqlDbToRedisDbAsync();
            await gameInfoRepository.MigrateSqlDbToRedisDbAsync();
            await gamelogsMd5CacheRepository.MigrateSqlDbToRedisDbAsync();
            await gameMerchantRepository.MigrateSqlDbToRedisDbAsync();
            await gameUsersRepository.MigrateSqlDbToRedisDbAsync();
            await gameLogsLotteryRepository.MigrateSqlDbToRedisDbAsync();

            // 6. Users
            await userHierarchyRepository.MigrateSqlDbToRedisDbAsync();
            await usersBankRepository.MigrateSqlDbToRedisDbAsync();
            await usersFundsRepository.MigrateSqlDbToRedisDbAsync();
            await usersRepository.MigrateSqlDbToRedisDbAsync();
            await usersSessionRepository.MigrateSqlDbToRedisDbAsync();
        }










    }
}
