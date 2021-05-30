public sealed class ExcelVariableDef
{
   public sealed class ability
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 能力名称</summary>
       public const int name = 1;
       /// <summary>[Int] 击敌回血</summary>
       public const int recover = 2;
       /// <summary>[Float] 子弹伤害提升</summary>
       public const int damageRate = 3;
       /// <summary>[Int] 血上限提升</summary>
       public const int maxHp = 4;
       /// <summary>[Int] 飞船速度加成</summary>
       public const int moveSpeed = 5;
       /// <summary>[Float] 射速加成</summary>
       public const int shotSpeed = 6;
       /// <summary>[Int] 子弹移速提升</summary>
       public const int bulletSpeed = 7;
       /// <summary>[Int] 每次子弹发射波数增加</summary>
       public const int BattleWave = 8;
       /// <summary>[Int] 每次发射子弹数目增加</summary>
       public const int BattleNumber = 9;
       /// <summary>[Int] 次数</summary>
       public const int count = 10;
   }
   public sealed class ach
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 图标</summary>
       public const int spriteName = 1;
       /// <summary>[Int] 类型</summary>
       public const int type = 2;
       /// <summary>[Int] 需求次数</summary>
       public const int num = 3;
       /// <summary>[Int] 奖励</summary>
       public const int expPasture = 4;
       /// <summary>[Int] 奖励</summary>
       public const int rewardMoney = 5;
       /// <summary>[Int] 成就分组</summary>
       public const int achgroup = 6;
       /// <summary>[Int] 成就显示类型</summary>
       public const int achType = 7;
       /// <summary>[Int] 奖励</summary>
       public const int rewardItmeid = 8;
       /// <summary>[String] 获得某物品</summary>
       public const int needItemidList = 9;
   }
   public sealed class actShopDay
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名称</summary>
       public const int name = 1;
       /// <summary>[String] 物品id</summary>
       public const int itemidList = 2;
       /// <summary>[String] 物品数量</summary>
       public const int pointList = 3;
       /// <summary>[String] 物品钻石价格</summary>
       public const int money = 4;
   }
   public sealed class actShopRose
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名称</summary>
       public const int name = 1;
       /// <summary>[String] 活动对应的积分图标</summary>
       public const int actIcon = 2;
       /// <summary>[String] 物品id</summary>
       public const int itemidList = 3;
       /// <summary>[String] 物品兑换积分</summary>
       public const int pointList = 4;
       /// <summary>[Int] 活动内容对应种子；</summary>
       public const int actSeed = 5;
       /// <summary>[Int] 单个种子价格</summary>
       public const int money = 6;
       /// <summary>[Int] 售卖花朵积分</summary>
       public const int pointSell = 7;
   }
   public sealed class air
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int Name = 1;
       /// <summary>[Int] 移动速度</summary>
       public const int Speed = 2;
       /// <summary>[Int] 血量</summary>
       public const int Hp = 3;
       /// <summary>[Int] 子弹ID</summary>
       public const int bulletId = 4;
       /// <summary>[Int] 出现难度</summary>
       public const int EmergeDegree = 5;
   }
   public sealed class airScene
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int Name = 1;
       /// <summary>[Int] 移动速度</summary>
       public const int Speed = 2;
       /// <summary>[Int] 飞机间隔</summary>
       public const int Hp = 3;
       /// <summary>[Int] 出现数量</summary>
       public const int num = 4;
       /// <summary>[Int] 出现数量</summary>
       public const int num2 = 5;
       /// <summary>[String] 出现类型</summary>
       public const int typeList = 6;
       /// <summary>[String] 曲线点</summary>
       public const int curveList = 7;
       /// <summary>[String] 偏移</summary>
       public const int offset = 8;
       /// <summary>[String] 飞行类型</summary>
       public const int type = 9;
       /// <summary>[String] scene</summary>
       public const int sceneName = 10;
       /// <summary>[Int] boss</summary>
       public const int boss = 11;
   }
   public sealed class animal
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] </summary>
       public const int prefab = 2;
       /// <summary>[Int] 出售价格</summary>
       public const int sellPrice = 3;
       /// <summary>[String] 主纹理</summary>
       public const int mainTexture = 4;
       /// <summary>[String] 无毛时的贴图材质</summary>
       public const int hairlessTexPath = 5;
       /// <summary>[Int] 移动空闲概率</summary>
       public const int movePer = 6;
       /// <summary>[Int] 年龄</summary>
       public const int year = 7;
       /// <summary>[Int] </summary>
       public const int year2 = 8;
       /// <summary>[Int] </summary>
       public const int level = 9;
       /// <summary>[Int] </summary>
       public const int level2 = 10;
       /// <summary>[Int] </summary>
       public const int level3 = 11;
       /// <summary>[Int] </summary>
       public const int level4 = 12;
       /// <summary>[Int] </summary>
       public const int pick = 13;
       /// <summary>[Float] 移动速度</summary>
       public const int speed = 14;
       /// <summary>[Float] 跑的速度</summary>
       public const int runSpeed = 15;
       /// <summary>[Int] 飞的速度</summary>
       public const int flySpeed = 16;
       /// <summary>[Float] 出生的X坐标</summary>
       public const int birthX = 17;
       /// <summary>[Float] </summary>
       public const int birthZ = 18;
       /// <summary>[Int] 随机位置区域</summary>
       public const int buyXStart = 19;
       /// <summary>[Int] </summary>
       public const int buyZStart = 20;
       /// <summary>[Int] </summary>
       public const int buyXEnd = 21;
       /// <summary>[Int] </summary>
       public const int buyZEnd = 22;
       /// <summary>[Int] 是否生病</summary>
       public const int ill = 23;
       /// <summary>[Float] 模型缩放比例_3D</summary>
       public const int modelScale_3D = 24;
       /// <summary>[Float] 模型缩放比例_Default</summary>
       public const int modelScale_Default = 25;
       /// <summary>[Float] 碰撞体大小</summary>
       public const int coliSizeX = 26;
       /// <summary>[Float] </summary>
       public const int coliSizeY = 27;
       /// <summary>[Float] </summary>
       public const int coliSizeZ = 28;
       /// <summary>[Float] 碰撞体偏移</summary>
       public const int coliOffsetX = 29;
       /// <summary>[Float] </summary>
       public const int coliOffsetY = 30;
       /// <summary>[Float] </summary>
       public const int coliOffsetZ = 31;
       /// <summary>[Int] 生长时间</summary>
       public const int growup = 32;
       /// <summary>[Int] 长成为</summary>
       public const int growupid = 33;
   }
   public sealed class animalEffect
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 爱心挂点</summary>
       public const int lovePath = 2;
       /// <summary>[String] 爱心1位置</summary>
       public const int tranLove1 = 3;
       /// <summary>[String] 爱心1缩放</summary>
       public const int scaleLove1 = 4;
       /// <summary>[String] 爱心2位置</summary>
       public const int tranlove2 = 5;
       /// <summary>[String] 爱心2缩放</summary>
       public const int scaleLove2 = 6;
       /// <summary>[String] 刷毛位置</summary>
       public const int tranBrush = 7;
       /// <summary>[String] 刷毛缩放</summary>
       public const int scaleBrush = 8;
       /// <summary>[String] 剪毛位置</summary>
       public const int tranHair = 9;
       /// <summary>[String] 剪毛缩放</summary>
       public const int scaleHair = 10;
       /// <summary>[String] 挤奶smoke位置</summary>
       public const int tranMilkSmoke = 11;
       /// <summary>[String] 挤奶smoke缩放</summary>
       public const int scaleMilkSmoke = 12;
       /// <summary>[String] 挤奶milk1位置</summary>
       public const int tranMilk1 = 13;
       /// <summary>[String] 挤奶milk1缩放</summary>
       public const int scaleMilk1 = 14;
       /// <summary>[String] 挤奶milk2位置</summary>
       public const int tranMilk2 = 15;
       /// <summary>[String] 挤奶milk2缩放</summary>
       public const int scaleMilk2 = 16;
   }
   public sealed class apple
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] rmb价格</summary>
       public const int rmbprice = 1;
       /// <summary>[Float] usa价格</summary>
       public const int usaprice = 2;
       /// <summary>[Int] vip经验</summary>
       public const int vipexp = 3;
       /// <summary>[Int] 钻石数值</summary>
       public const int money = 4;
   }
   public sealed class battle
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 子弹名字</summary>
       public const int ChineseSimplified = 1;
       /// <summary>[Float] 子弹发射频率，一秒发射几次</summary>
       public const int BattleFrequency = 2;
       /// <summary>[Int] 子弹移动速度</summary>
       public const int BattleSpeed = 3;
       /// <summary>[Int] 每次发射子弹数量</summary>
       public const int BattleNumber = 4;
       /// <summary>[Int] 子弹伤害</summary>
       public const int BattleDamage = 5;
       /// <summary>[Int] 是否是玩家子弹</summary>
       public const int PlayerOrNot = 6;
       /// <summary>[Float] 时间</summary>
       public const int Time = 7;
       /// <summary>[Int] 类型</summary>
       public const int Type = 8;
       /// <summary>[Int] 击中是否消失</summary>
       public const int Disapear = 9;
   }
   public sealed class birthPos
   {
       /// <summary>[Int] 场景ID</summary>
       public const int id = 0;
       /// <summary>[String] 场景名字</summary>
       public const int sceneName = 1;
       /// <summary>[String] 可以去的场景</summary>
       public const int sceneIDList = 2;
       /// <summary>[String] 可去场景的出生点</summary>
       public const int scenePortList = 3;
       /// <summary>[String] 可取场景出生点角色朝向</summary>
       public const int scenePortRotList = 4;
   }
   public sealed class boxOpen
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int itemidList = 1;
       /// <summary>[String] </summary>
       public const int perList = 2;
       /// <summary>[String] 各物品数量</summary>
       public const int numitems = 3;
       /// <summary>[Int] </summary>
       public const int randomMax = 4;
   }
   public sealed class build
   {
       /// <summary>[Int] 建筑编号</summary>
       public const int id = 0;
       /// <summary>[String] 建筑名称</summary>
       public const int content = 1;
       /// <summary>[Int] 图鉴分类</summary>
       public const int illuType = 2;
       /// <summary>[Int] 升级需要木材</summary>
       public const int needWood = 3;
       /// <summary>[Int] 升级需要铁矿</summary>
       public const int needIron = 4;
       /// <summary>[Int] 升级需要铜矿</summary>
       public const int needCopper = 5;
       /// <summary>[Int] 升级需要银矿</summary>
       public const int needSliver = 6;
       /// <summary>[Int] 升级需要金币</summary>
       public const int needCoin = 7;
       /// <summary>[String] 介绍</summary>
       public const int information = 8;
       /// <summary>[Int] 类型</summary>
       public const int type = 9;
       /// <summary>[Int] 所属场景</summary>
       public const int scene = 10;
       /// <summary>[Int] 所属场景编号</summary>
       public const int sceneNum = 11;
       /// <summary>[Int] 建筑等级</summary>
       public const int buildLevel = 12;
       /// <summary>[String] 皮肤名称</summary>
       public const int matetial = 13;
       /// <summary>[String] 原画样式</summary>
       public const int painting = 14;
       /// <summary>[String] 3D样式</summary>
       public const int model = 15;
       /// <summary>[Int] 图鉴线类型</summary>
       public const int lineType = 16;
       /// <summary>[String] 图鉴图名</summary>
       public const int spriteName = 17;
       /// <summary>[Int] 升级需求类型</summary>
       public const int upType = 18;
       /// <summary>[Int] 信息位置</summary>
       public const int pointType = 19;
       /// <summary>[String] 3d物品名字</summary>
       public const int prefabName = 20;
   }
   public sealed class buyGold
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 获得金币</summary>
       public const int gold = 1;
       /// <summary>[Int] 花费钻石</summary>
       public const int money = 2;
       /// <summary>[Int] 第几次购买</summary>
       public const int count = 3;
       /// <summary>[Int] 对应牧场等级</summary>
       public const int start = 4;
       /// <summary>[Int] 对应牧场等级</summary>
       public const int end = 5;
   }
   public sealed class buyIvary
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 第几次购买象牙球</summary>
       public const int count = 1;
       /// <summary>[Int] 象牙球消耗钻石数量</summary>
       public const int money = 2;
       /// <summary>[String] 不同VIP可以购买的次数上限</summary>
       public const int vipartballnum = 3;
   }
   public sealed class buyLure
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] </summary>
       public const int condition = 2;
       /// <summary>[Float] </summary>
       public const int dollar = 3;
       /// <summary>[Int] 购买次数</summary>
       public const int buyLimite = 4;
       /// <summary>[Int] 倒计时毫秒单位</summary>
       public const int timeLimite = 5;
       /// <summary>[Int] 一天只出一次</summary>
       public const int dayOne = 6;
       /// <summary>[Int] 今后重复出现</summary>
       public const int repeat = 7;
       /// <summary>[Int] 金币</summary>
       public const int gold = 8;
       /// <summary>[Int] 钻石</summary>
       public const int money = 9;
       /// <summary>[Int] 装备经验</summary>
       public const int equipExp = 10;
       /// <summary>[Int] 普通牛</summary>
       public const int feed = 11;
       /// <summary>[Int] 高级牛</summary>
       public const int feed2 = 12;
       /// <summary>[Int] 普通鸡</summary>
       public const int feed3 = 13;
       /// <summary>[Int] 高级鸡</summary>
       public const int feed4 = 14;
       /// <summary>[Int] 象牙球</summary>
       public const int ivary = 15;
       /// <summary>[Int] 精灵经验</summary>
       public const int spriteExp = 16;
       /// <summary>[Int] </summary>
       public const int itemid = 17;
       /// <summary>[Int] 种子季节</summary>
       public const int seedSeason = 18;
       /// <summary>[Int] 种子季节数量</summary>
       public const int seedSeasonNum = 19;
       /// <summary>[Int] 苹果内购商品编号</summary>
       public const int appleid = 20;
   }
   public sealed class buyMoney
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 礼包类型/名称</summary>
       public const int name = 1;
       /// <summary>[Int] 人民币售卖价格</summary>
       public const int rmbmoney = 2;
       /// <summary>[Float] 美元售卖价格</summary>
       public const int usamoney = 3;
       /// <summary>[Int] 购买后单次获得钻石数量</summary>
       public const int money = 4;
       /// <summary>[Int] 购买后获得VIP经验</summary>
       public const int vipexp = 5;
       /// <summary>[Int] 是否存在首次双倍</summary>
       public const int doublemoney = 6;
       /// <summary>[Int] 苹果内购的id编号</summary>
       public const int appleItemid = 7;
   }
   public sealed class buyMoneyBargain
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 超值礼包中售卖的物品id</summary>
       public const int itemidList = 1;
       /// <summary>[String] 超值礼包中售卖的物品数量</summary>
       public const int numList = 2;
       /// <summary>[Int] 该次礼包中的积分编号</summary>
       public const int weekid = 3;
       /// <summary>[Int] 该次礼包中的积分数量</summary>
       public const int weekPoint = 4;
       /// <summary>[Int] 该礼包对应的苹果内购编号</summary>
       public const int appleItemid = 5;
       /// <summary>[Int] 礼包限购次数</summary>
       public const int count = 6;
       /// <summary>[Int] 礼包持续天数</summary>
       public const int day = 7;
   }
   public sealed class buyMoneyEquip
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 活动名称</summary>
       public const int actname = 1;
       /// <summary>[Int] 奖励物品内容</summary>
       public const int itemid = 2;
       /// <summary>[Int] 奖励特殊产品内容</summary>
       public const int itemspid = 3;
       /// <summary>[Int] 苹果内购产品编号</summary>
       public const int appleItemid = 4;
       /// <summary>[Int] 限购次数</summary>
       public const int count = 5;
       /// <summary>[Int] 活动持续天数</summary>
       public const int day = 6;
   }
   public sealed class camera
   {
       /// <summary>[Int] 场景ID</summary>
       public const int id = 0;
       /// <summary>[String] 场景名字</summary>
       public const int sceneName = 1;
       /// <summary>[Int] 是否是固定角度</summary>
       public const int isFixed = 2;
       /// <summary>[Float] 摄像机X</summary>
       public const int posX = 3;
       /// <summary>[Float] 摄像机Y</summary>
       public const int posY = 4;
       /// <summary>[Float] 摄像机Z</summary>
       public const int posZ = 5;
       /// <summary>[Float] 摄像机旋转X</summary>
       public const int rotX = 6;
       /// <summary>[Float] 摄像机旋转Y</summary>
       public const int rotY = 7;
       /// <summary>[Float] 摄像机旋转Z</summary>
       public const int rotZ = 8;
       /// <summary>[Int] 摄像机FOV</summary>
       public const int fov = 9;
       /// <summary>[Int] 摄像机透视</summary>
       public const int projection = 10;
   }
   public sealed class cameraMoveByPic
   {
       /// <summary>[Int] ID(XY坐标)</summary>
       public const int id = 0;
       /// <summary>[Int] RG颜色值</summary>
       public const int rgValue = 1;
   }
   public sealed class client3DConfig
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 山货每日随机数</summary>
       public const int forestProductsResetNum = 1;
       /// <summary>[Int] 水产每日随机数</summary>
       public const int aquaticResetNum = 2;
       /// <summary>[Int] 菜肴每日随机数</summary>
       public const int dishesResetNum = 3;
       /// <summary>[Int] 蔬菜种子每日随机数</summary>
       public const int vegSeedResetNum = 4;
       /// <summary>[Int] 出货时间</summary>
       public const int sellTime = 5;
   }
   public sealed class configE
   {
       /// <summary>[Int] 格子ID</summary>
       public const int id = 0;
       /// <summary>[String] 127.0.0.1</summary>
       public const int content = 1;
       /// <summary>[Int] 交易单天数</summary>
       public const int tradeOver = 2;
       /// <summary>[Int] 背包初始</summary>
       public const int itemStart = 3;
       /// <summary>[Int] 鸡舍最大等级</summary>
       public const int chHomeMax = 4;
       /// <summary>[Int] 牛舍最大等级</summary>
       public const int cowHomeMax = 5;
       /// <summary>[Int] 主屋最大等级</summary>
       public const int homeMax = 6;
       /// <summary>[Int] 背包最大</summary>
       public const int itemMax = 7;
       /// <summary>[Int] 储物柜最大</summary>
       public const int bagMax = 8;
       /// <summary>[Int] 可叠加最大</summary>
       public const int imposeMax = 9;
       /// <summary>[Int] 菜地木头</summary>
       public const int wood = 10;
       /// <summary>[Int] 菜地木头</summary>
       public const int wood2 = 11;
       /// <summary>[Int] </summary>
       public const int stone1x1 = 12;
       /// <summary>[Int] </summary>
       public const int stone2x2 = 13;
       /// <summary>[Int] </summary>
       public const int wood1x1 = 14;
       /// <summary>[Int] </summary>
       public const int num3x3 = 15;
       /// <summary>[Int] </summary>
       public const int grass1x1 = 16;
       /// <summary>[Int] 每天杂草概率</summary>
       public const int everyGrassPer = 17;
       /// <summary>[Int] </summary>
       public const int grassPerMax = 18;
       /// <summary>[Int] </summary>
       public const int stone1x1Start = 19;
       /// <summary>[Int] </summary>
       public const int stone1x1End = 20;
       /// <summary>[Int] </summary>
       public const int wood1x1Start = 21;
       /// <summary>[Int] </summary>
       public const int wood1x1End = 22;
       /// <summary>[Int] </summary>
       public const int grass1x1Start = 23;
       /// <summary>[Int] </summary>
       public const int grass1x1End = 24;
       /// <summary>[Int] </summary>
       public const int stone2x2Per = 25;
       /// <summary>[Int] </summary>
       public const int stone3x3Per = 26;
       /// <summary>[Int] </summary>
       public const int wood3x3Per = 27;
       /// <summary>[Int] </summary>
       public const int ivaryMax = 28;
       /// <summary>[Int] 象牙</summary>
       public const int ivaryStart = 29;
       /// <summary>[Int] 可赠送的象牙上限</summary>
       public const int ivarySendMax = 30;
       /// <summary>[Int] 可领取的象牙上限</summary>
       public const int ivaryGetMax = 31;
       /// <summary>[Int] 单人最大交易数</summary>
       public const int tradeMax = 32;
       /// <summary>[Int] 交易历史记录上限</summary>
       public const int tradeHMax = 33;
       /// <summary>[Int] </summary>
       public const int reqMax = 34;
       /// <summary>[Int] 每天添加最大</summary>
       public const int efAddMax = 35;
       /// <summary>[Int] 好友上限</summary>
       public const int friendMax = 36;
       /// <summary>[Int] 好友申请显示上限</summary>
       public const int friendReqMax = 37;
       /// <summary>[Int] 推荐好友显示上限</summary>
       public const int friendReComMax = 38;
       /// <summary>[Int] 好友拜访上限</summary>
       public const int friendVisitMax = 39;
       /// <summary>[Int] 聊天</summary>
       public const int chatMax = 40;
       /// <summary>[Int] 邮件最大</summary>
       public const int mailMax = 41;
       /// <summary>[Int] 毫秒</summary>
       public const int oneHour = 42;
       /// <summary>[Int] 饲料上限</summary>
       public const int feedMax = 43;
       /// <summary>[Int] 初始牛口</summary>
       public const int feed = 44;
       /// <summary>[Int] 初始鸡口</summary>
       public const int feed3 = 45;
       /// <summary>[Int] 生病天</summary>
       public const int illDay = 46;
       /// <summary>[Int] 生病死亡天</summary>
       public const int deathDay = 47;
       /// <summary>[Int] </summary>
       public const int restorePer = 48;
       /// <summary>[Int] 野猪攻击</summary>
       public const int pigPer = 49;
       /// <summary>[Int] gm邮件最大</summary>
       public const int gmMailHMax = 50;
       /// <summary>[Int] 牛怀胎期</summary>
       public const int pregCow = 51;
       /// <summary>[Int] 羊怀胎期</summary>
       public const int pregSheep = 52;
       /// <summary>[Int] 鸡怀胎期</summary>
       public const int pregCh = 53;
       /// <summary>[Int] 羊寿命</summary>
       public const int lifeSheep = 54;
       /// <summary>[Int] 鸡寿命</summary>
       public const int lifeCh = 55;
       /// <summary>[Int] 牛寿命</summary>
       public const int lifeCow = 56;
       /// <summary>[Int] 友好度最大</summary>
       public const int aniFriendMax = 57;
       /// <summary>[Int] 友好度最大</summary>
       public const int spritefriendMax = 58;
       /// <summary>[Int] 糖果最大</summary>
       public const int sugarMax = 59;
       /// <summary>[Int] 帮忙天数</summary>
       public const int helpDay = 60;
       /// <summary>[Int] 体力最大</summary>
       public const int phyMax = 61;
       /// <summary>[Int] 体力初始最大</summary>
       public const int phyMaxStart = 62;
       /// <summary>[Int] 金币初始</summary>
       public const int goldStart = 63;
       /// <summary>[Int] 金币最大</summary>
       public const int goldMax = 64;
       /// <summary>[Int] 一颗糖经验</summary>
       public const int sugarExp = 65;
       /// <summary>[Int] 体力警告</summary>
       public const int phyWarn = 66;
       /// <summary>[Int] 变荒地概率</summary>
       public const int perWastland = 67;
       /// <summary>[Int] </summary>
       public const int chOldStart = 68;
       /// <summary>[Int] </summary>
       public const int chOldEnd = 69;
       /// <summary>[Int] </summary>
       public const int cowOldStart = 70;
       /// <summary>[Int] </summary>
       public const int cowOldEnd = 71;
       /// <summary>[Int] </summary>
       public const int buyAnimalFriendCh = 72;
       /// <summary>[Int] </summary>
       public const int buyAnimalFriendCow = 73;
       /// <summary>[Int] 好感对应心数量（鸡）</summary>
       public const int oneHeartCh = 74;
       /// <summary>[Int] 好感对应心数量（牛羊）</summary>
       public const int oneHeartCow = 75;
       /// <summary>[Int] 好感对应心数量（狗）</summary>
       public const int oneHeartDog = 76;
       /// <summary>[Int] 好感对应心数量（马）</summary>
       public const int oneHeartHorse = 77;
       /// <summary>[Int] 黑毛羊概率</summary>
       public const int blackSheepPer = 78;
       /// <summary>[Int] 生日送礼额外好感</summary>
       public const int birthdayFriend = 79;
       /// <summary>[Int] 不开心吃饭后恢复概率</summary>
       public const int perAniRestore = 80;
       /// <summary>[Int] 可显示星星槽位</summary>
       public const int starCount = 81;
   }
   public sealed class cook
   {
       /// <summary>[Int] 物品id</summary>
       public const int id = 0;
       /// <summary>[String] 必备材料A</summary>
       public const int itemList = 1;
       /// <summary>[String] 必备材料B</summary>
       public const int itemList2 = 2;
       /// <summary>[String] 必须材料C</summary>
       public const int itemList3 = 3;
       /// <summary>[Int] 最少有几种C</summary>
       public const int needNumStart3 = 4;
       /// <summary>[Int] 最多有几种C</summary>
       public const int needNumEnd3 = 5;
       /// <summary>[Int] 必要食材一共最少有多少种数量</summary>
       public const int needNumberAllStart = 6;
       /// <summary>[Int] 必要食材一共最少有多少种数量</summary>
       public const int needNumberAllEnd = 7;
       /// <summary>[String] 附加食材</summary>
       public const int assitItem = 8;
       /// <summary>[Int] 必要厨具A</summary>
       public const int kitchen = 9;
       /// <summary>[Int] 必要厨具B</summary>
       public const int kitchen2 = 10;
       /// <summary>[Int] 附加厨具</summary>
       public const int kitchenAssit = 11;
   }
   public sealed class curve
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] pos</summary>
       public const int pos = 1;
   }
   public sealed class effect
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 特效名字</summary>
       public const int name = 1;
       /// <summary>[String] 特效路径</summary>
       public const int path = 2;
       /// <summary>[String] 坐标变量名</summary>
       public const int tranParm = 3;
       /// <summary>[String] 缩放变量mignon</summary>
       public const int scaleParm = 4;
   }
   public sealed class egg
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int name = 1;
       /// <summary>[Int] </summary>
       public const int itemid = 2;
       /// <summary>[Int] </summary>
       public const int itemid2 = 3;
       /// <summary>[Int] </summary>
       public const int per = 4;
       /// <summary>[Int] </summary>
       public const int itemid3 = 5;
       /// <summary>[Int] </summary>
       public const int itemid4 = 6;
   }
   public sealed class eggBox
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int itemidList = 1;
       /// <summary>[String] </summary>
       public const int numList = 2;
       /// <summary>[String] </summary>
       public const int perList = 3;
       /// <summary>[String] 特殊化爆率</summary>
       public const int perBadList = 4;
       /// <summary>[Int] 保底数量</summary>
       public const int countLuck = 5;
       /// <summary>[Int] 保底数量对应的奖励内容</summary>
       public const int itemLuck = 6;
       /// <summary>[Int] 低爆率次数</summary>
       public const int countBad = 7;
       /// <summary>[Int] </summary>
       public const int randomMax = 8;
       /// <summary>[Int] </summary>
       public const int money = 9;
       /// <summary>[Int] </summary>
       public const int money10 = 10;
       /// <summary>[String] 扭蛋图标</summary>
       public const int eggImage = 11;
   }
   public sealed class equip
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 材质</summary>
       public const int material = 2;
       /// <summary>[Int] 大类</summary>
       public const int type = 3;
       /// <summary>[Float] 碰撞盒距离</summary>
       public const int distBox = 4;
       /// <summary>[Float] 碰撞盒大小</summary>
       public const int sizeBox = 5;
       /// <summary>[Int] 伤害值</summary>
       public const int hitValue = 6;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSizex = 7;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSizey = 8;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize2x = 9;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize2y = 10;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize3x = 11;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize3y = 12;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize4x = 13;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize4y = 14;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize5x = 15;
       /// <summary>[Int] 伤害范围</summary>
       public const int hitSize5y = 16;
       /// <summary>[Int] 伤害作用类型</summary>
       public const int hitSizeType1 = 17;
       /// <summary>[Int] 伤害作用类型</summary>
       public const int hitSizeType2 = 18;
       /// <summary>[Int] 伤害作用类型</summary>
       public const int hitSizeType3 = 19;
       /// <summary>[Int] 伤害作用类型</summary>
       public const int hitSizeType4 = 20;
       /// <summary>[Int] 伤害作用类型</summary>
       public const int hitSizeType5 = 21;
       /// <summary>[Int] 体力消耗</summary>
       public const int phy = 22;
       /// <summary>[Int] 体力消耗</summary>
       public const int phy2 = 23;
       /// <summary>[Int] 体力消耗</summary>
       public const int phy3 = 24;
       /// <summary>[Int] 体力消耗</summary>
       public const int phy4 = 25;
       /// <summary>[Int] 体力消耗</summary>
       public const int phy5 = 26;
       /// <summary>[Int] 等级</summary>
       public const int grade = 27;
       /// <summary>[Int] 角色旋转方式0无方向8八方向4四方向1面向动物10临时默认值</summary>
       public const int playerRotType = 28;
       /// <summary>[Int] 角色旋转方式0无方向8八方向4四方向1面向动物10临时默认值</summary>
       public const int playerRotType2 = 29;
       /// <summary>[Int] 角色旋转方式0无方向8八方向4四方向1面向动物10临时默认值</summary>
       public const int playerRotType3 = 30;
       /// <summary>[Int] 角色旋转方式0无方向8八方向4四方向1面向动物10临时默认值</summary>
       public const int playerRotType4 = 31;
       /// <summary>[Int] 角色旋转方式0无方向8八方向4四方向1面向动物10临时默认值</summary>
       public const int playerRotType5 = 32;
       /// <summary>[Int] 最大容量</summary>
       public const int maxCapacity = 33;
       /// <summary>[String] 手上偏移X_男</summary>
       public const int handPos_man = 34;
       /// <summary>[String] 手上旋转X_男</summary>
       public const int handRot_man = 35;
       /// <summary>[String] 手上缩放X_男</summary>
       public const int handScl_man = 36;
       /// <summary>[String] 腰上偏移X_男</summary>
       public const int waistPos_man = 37;
       /// <summary>[String] 腰上旋转Z_男</summary>
       public const int waistRot_man = 38;
       /// <summary>[String] 腰上缩放X_男</summary>
       public const int waistScl_man = 39;
       /// <summary>[String] 背上偏移X_男</summary>
       public const int backPos_man = 40;
       /// <summary>[String] 背上旋转X_男</summary>
       public const int backRot_man = 41;
       /// <summary>[String] 背上缩放X_男</summary>
       public const int backScl_man = 42;
       /// <summary>[String] 手上偏移X_女</summary>
       public const int handPos_woman = 43;
       /// <summary>[String] 手上旋转X_女</summary>
       public const int handRot_woman = 44;
       /// <summary>[String] 手上缩放X_女</summary>
       public const int handScl_woman = 45;
       /// <summary>[String] 腰上偏移X_女</summary>
       public const int waistPos_woman = 46;
       /// <summary>[String] 腰上旋转Z_女</summary>
       public const int waistRot_woman = 47;
       /// <summary>[String] 腰上缩放X_女</summary>
       public const int waistScl_woman = 48;
       /// <summary>[String] 背上偏移X_女</summary>
       public const int backPos_woman = 49;
       /// <summary>[String] 背上旋转X_女</summary>
       public const int backRot_woman = 50;
       /// <summary>[String] 背上缩放X_女</summary>
       public const int backScl_woman = 51;
   }
   public sealed class equipKillItem
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 石头3x3</summary>
       public const int stone3x3_0 = 2;
       /// <summary>[Int] 石头3x3</summary>
       public const int stone3x3_1 = 3;
       /// <summary>[Int] 石头3x3</summary>
       public const int stone3x3_2 = 4;
       /// <summary>[Int] 石头3x3</summary>
       public const int stone3x3_3 = 5;
       /// <summary>[Int] 石头3x3</summary>
       public const int stone3x3_4 = 6;
       /// <summary>[Int] 石头2x2</summary>
       public const int stone2x2_0 = 7;
       /// <summary>[Int] 石头2x2</summary>
       public const int stone2x2_1 = 8;
       /// <summary>[Int] 石头2x2</summary>
       public const int stone2x2_2 = 9;
       /// <summary>[Int] 石头2x2</summary>
       public const int stone2x2_3 = 10;
       /// <summary>[Int] 石头2x2</summary>
       public const int stone2x2_4 = 11;
       /// <summary>[Int] 石头1x1</summary>
       public const int stone1x1_0 = 12;
       /// <summary>[Int] 石头1x1</summary>
       public const int stone1x1_1 = 13;
       /// <summary>[Int] 石头1x1</summary>
       public const int stone1x1_2 = 14;
       /// <summary>[Int] 石头1x1</summary>
       public const int stone1x1_3 = 15;
       /// <summary>[Int] 石头1x1</summary>
       public const int stone1x1_4 = 16;
       /// <summary>[Int] 木头3x3</summary>
       public const int wood3x3_0 = 17;
       /// <summary>[Int] 木头3x3</summary>
       public const int wood3x3_1 = 18;
       /// <summary>[Int] 木头3x3</summary>
       public const int wood3x3_2 = 19;
       /// <summary>[Int] 木头3x3</summary>
       public const int wood3x3_3 = 20;
       /// <summary>[Int] 木头3x3</summary>
       public const int wood3x3_4 = 21;
       /// <summary>[Int] 木头1x1</summary>
       public const int wood1x1_0 = 22;
       /// <summary>[Int] 木头1x1</summary>
       public const int wood1x1_1 = 23;
       /// <summary>[Int] 木头1x1</summary>
       public const int wood1x1_2 = 24;
       /// <summary>[Int] 木头1x1</summary>
       public const int wood1x1_3 = 25;
       /// <summary>[Int] 木头1x1</summary>
       public const int wood1x1_4 = 26;
   }
   public sealed class equipSkilled
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 到该等级需要的总经验</summary>
       public const int upExp = 1;
       /// <summary>[Int] 铁</summary>
       public const int ironLevel = 2;
       /// <summary>[Int] 铜</summary>
       public const int copperLevel = 3;
       /// <summary>[Int] 银</summary>
       public const int silverLevel = 4;
       /// <summary>[Int] 金</summary>
       public const int goldLevel = 5;
       /// <summary>[Int] 秘银</summary>
       public const int secretSilverLevel = 6;
       /// <summary>[Int] 最大等级</summary>
       public const int maxLv = 7;
   }
   public sealed class equipType
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int typeName = 1;
       /// <summary>[Int] EnumType</summary>
       public const int enumType = 2;
       /// <summary>[Int] shopEqId</summary>
       public const int shopEqId = 3;
       /// <summary>[Int] 初始id</summary>
       public const int eqID = 4;
       /// <summary>[String] 图名</summary>
       public const int sprName = 5;
       /// <summary>[String] itemList</summary>
       public const int itemList = 6;
   }
   public sealed class expPasture
   {
       /// <summary>[Int] 格子ID</summary>
       public const int id = 0;
       /// <summary>[Int] 累积经验</summary>
       public const int exp = 1;
   }
   public sealed class farmlv
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int name = 1;
       /// <summary>[Int] 农场等级</summary>
       public const int farmlv0 = 2;
       /// <summary>[Int] 每级升级所需经验</summary>
       public const int farmexp = 3;
   }
   public sealed class festival
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 季节</summary>
       public const int season = 2;
       /// <summary>[Int] 天</summary>
       public const int day = 3;
       /// <summary>[Int] 小时</summary>
       public const int hour = 4;
       /// <summary>[Int] 固定的奖励物品</summary>
       public const int itemid = 5;
   }
   public sealed class fishing
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 河鱼春</summary>
       public const int fish = 1;
       /// <summary>[String] 夏</summary>
       public const int fish2 = 2;
       /// <summary>[String] 秋</summary>
       public const int fish3 = 3;
       /// <summary>[String] 冬</summary>
       public const int fish4 = 4;
       /// <summary>[String] 海鱼春天</summary>
       public const int fish5 = 5;
       /// <summary>[String] 夏</summary>
       public const int fish6 = 6;
       /// <summary>[String] 秋</summary>
       public const int fish7 = 7;
       /// <summary>[String] 冬</summary>
       public const int fish8 = 8;
   }
   public sealed class fishingPer
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] </summary>
       public const int randomMax = 1;
       /// <summary>[Int] </summary>
       public const int perStart = 2;
       /// <summary>[Int] </summary>
       public const int perEnd = 3;
       /// <summary>[Int] </summary>
       public const int perStart2 = 4;
       /// <summary>[Int] </summary>
       public const int perEnd2 = 5;
       /// <summary>[Int] </summary>
       public const int perStart3 = 6;
       /// <summary>[Int] </summary>
       public const int perEnd3 = 7;
       /// <summary>[Int] </summary>
       public const int perStart4 = 8;
       /// <summary>[Int] </summary>
       public const int perEnd4 = 9;
       /// <summary>[Int] </summary>
       public const int perStart5 = 10;
       /// <summary>[Int] </summary>
       public const int perEnd5 = 11;
       /// <summary>[Int] </summary>
       public const int perStart6 = 12;
       /// <summary>[Int] </summary>
       public const int perEnd6 = 13;
   }
   public sealed class fishProb
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Float] 稀有度1的概率</summary>
       public const int rarity1 = 1;
       /// <summary>[Float] 稀有度2的概率</summary>
       public const int rarity2 = 2;
       /// <summary>[Float] 稀有度3的概率</summary>
       public const int rarity3 = 3;
       /// <summary>[Float] 稀有度2稀有种的概率</summary>
       public const int rarity2rar = 4;
       /// <summary>[Float] 稀有度3稀有种的概率</summary>
       public const int rarity3rar = 5;
       /// <summary>[Float] 稀有度4稀有种的概率</summary>
       public const int rarity4rar = 6;
   }
   public sealed class fishType
   {
       /// <summary>[Int] item编号</summary>
       public const int id = 0;
       /// <summary>[Int] 鱼大小归属</summary>
       public const int typeid = 1;
       /// <summary>[Int] 长度范围小</summary>
       public const int sizesmall = 2;
       /// <summary>[Int] 长度范围大</summary>
       public const int sizebig = 3;
   }
   public sealed class game
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 小游戏名称</summary>
       public const int name = 1;
   }
   public sealed class gameList
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] </summary>
       public const int ip = 2;
       /// <summary>[Int] 第一行表示版本</summary>
       public const int port = 3;
   }
   public sealed class gen
   {
       /// <summary>[Int] 基因编号</summary>
       public const int id = 0;
       /// <summary>[Float] 羊大小缩放</summary>
       public const int sheepSize = 1;
       /// <summary>[Float] 牛大小缩放</summary>
       public const int cowSize = 2;
       /// <summary>[Int] 牛的大块颜色</summary>
       public const int cowLR = 3;
       /// <summary>[Int] </summary>
       public const int cowLG = 4;
       /// <summary>[Int] </summary>
       public const int cowLB = 5;
       /// <summary>[Int] 羊脸部颜色</summary>
       public const int sheepFaceR = 6;
       /// <summary>[Int] </summary>
       public const int sheepFaceG = 7;
       /// <summary>[Int] </summary>
       public const int sheepFaceB = 8;
       /// <summary>[Int] 羊身子颜色</summary>
       public const int sheepBodyR = 9;
       /// <summary>[Int] </summary>
       public const int sheepBodyG = 10;
       /// <summary>[Int] </summary>
       public const int sheepBodyB = 11;
       /// <summary>[Int] 牛的小块颜色</summary>
       public const int cowMR = 12;
       /// <summary>[Int] </summary>
       public const int cowMG = 13;
       /// <summary>[Int] </summary>
       public const int cowMB = 14;
       /// <summary>[String] 小牛尾巴路径</summary>
       public const int cowYgTailPath = 15;
       /// <summary>[String] 小牛耳朵路径</summary>
       public const int cowYgEarPath = 16;
       /// <summary>[String] 成年牛尾巴路径</summary>
       public const int cowTailPath = 17;
       /// <summary>[String] 成年牛耳朵路径</summary>
       public const int cowEarPath = 18;
       /// <summary>[String] 小牛图案样式</summary>
       public const int cowYgMaskPath = 19;
       /// <summary>[String] 成年牛图案样式路径</summary>
       public const int cowMaskPath = 20;
       /// <summary>[String] 成年羊身子路径</summary>
       public const int sheepBodyPath = 21;
       /// <summary>[String] 成年羊耳朵路径</summary>
       public const int sheepEarPath = 22;
       /// <summary>[String] 成年羊图案样式</summary>
       public const int sheepMaskPath = 23;
       /// <summary>[String] 成年无毛羊团样式</summary>
       public const int sheepHairlessMaskPath = 24;
       /// <summary>[String] 小羊耳朵路径</summary>
       public const int sheepYgEarPath = 25;
       /// <summary>[String] 小羊图案样式</summary>
       public const int sheepYgMaskPath = 26;
   }
   public sealed class groundType
   {
       /// <summary>[Int] ID(XZ坐标)</summary>
       public const int id = 0;
       /// <summary>[Int] 地面类型</summary>
       public const int groundType0 = 1;
   }
   public sealed class hole
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 矿等于物品id</summary>
       public const int oreidList = 2;
       /// <summary>[Int] 出物品概率</summary>
       public const int per = 3;
       /// <summary>[String] 矿等于物品id</summary>
       public const int oreidList2 = 4;
       /// <summary>[Int] 出物品概率</summary>
       public const int per2 = 5;
       /// <summary>[Int] 锄地上限</summary>
       public const int ladderCount = 6;
       /// <summary>[Int] 梯子出现概率</summary>
       public const int ladderPer = 7;
       /// <summary>[String] 矿洞大小</summary>
       public const int oreAreaSize = 8;
       /// <summary>[Int] 矿石数量</summary>
       public const int oreCount = 9;
       /// <summary>[Int] 宝石数量</summary>
       public const int gemOreCount = 10;
       /// <summary>[Int] 出生位置(索引)</summary>
       public const int birthGrid = 11;
       /// <summary>[Int] 出口位置</summary>
       public const int exitIndex = 12;
       /// <summary>[Int] 场景</summary>
       public const int sceneID = 13;
   }
   public sealed class item
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] shopEqId</summary>
       public const int shopEqId = 2;
       /// <summary>[Int] 是否可以喝</summary>
       public const int isDrink = 3;
       /// <summary>[Int] 大类</summary>
       public const int type = 4;
       /// <summary>[Int] 小类</summary>
       public const int subType = 5;
       /// <summary>[Int] 模型UI缩放种类</summary>
       public const int scale3DId = 6;
       /// <summary>[Int] 柜子搜索分类</summary>
       public const int cupSelectType = 7;
       /// <summary>[Int] 图鉴分类</summary>
       public const int illuType = 8;
       /// <summary>[Int] 交易分类</summary>
       public const int tradeType = 9;
       /// <summary>[Int] 菜肴细分</summary>
       public const int cookType = 10;
       /// <summary>[Int] 能否放入背包</summary>
       public const int canBag = 11;
       /// <summary>[Int] 可食用</summary>
       public const int isEat = 12;
       /// <summary>[Int] 是否需要浇水 1不需要</summary>
       public const int cactus = 13;
       /// <summary>[Int] 售卖价格</summary>
       public const int price = 14;
       /// <summary>[Int] 叠加(客)</summary>
       public const int cImpose = 15;
       /// <summary>[Int] 叠加(服)</summary>
       public const int impose = 16;
       /// <summary>[Float] 速度</summary>
       public const int speed = 17;
       /// <summary>[Float] 碰撞盒距离</summary>
       public const int distBox = 18;
       /// <summary>[Float] 碰撞盒大小</summary>
       public const int sizeBox = 19;
       /// <summary>[Int] </summary>
       public const int dis = 20;
       /// <summary>[String] 预设体路径</summary>
       public const int prefab = 21;
       /// <summary>[String] 子物体材质</summary>
       public const int childMaterial = 22;
       /// <summary>[String] 菜地里的农作物的路径</summary>
       public const int cropPrefabPath = 23;
       /// <summary>[Int] 发芽</summary>
       public const int sprout = 24;
       /// <summary>[String] 生长阶段</summary>
       public const int growStep = 25;
       /// <summary>[String] 对应的土堆类型</summary>
       public const int moundType = 26;
       /// <summary>[Int] 成长期</summary>
       public const int grow = 27;
       /// <summary>[Int] 无限采摘</summary>
       public const int pluckLoop = 28;
       /// <summary>[Int] 季节</summary>
       public const int season = 29;
       /// <summary>[Int] 果实</summary>
       public const int fruit = 30;
       /// <summary>[String] 图名</summary>
       public const int spriteName = 31;
       /// <summary>[Int] 品质</summary>
       public const int qua = 32;
       /// <summary>[Int] 体力恢复</summary>
       public const int phy = 33;
       /// <summary>[Int] 交易倒计时</summary>
       public const int countDown = 34;
       /// <summary>[String] 3D物品名称</summary>
       public const int prefabName = 35;
       /// <summary>[Int] 小土堆</summary>
       public const int hillID = 36;
       /// <summary>[String] 土堆位置</summary>
       public const int hillPos = 37;
       /// <summary>[Float] 土堆旋转</summary>
       public const int hillRot = 38;
       /// <summary>[Float] 土堆大小</summary>
       public const int hillSca = 39;
       /// <summary>[String] 3DUI坐标</summary>
       public const int prefabPosition = 40;
       /// <summary>[String] 3DUI旋转</summary>
       public const int prefabRotation = 41;
       /// <summary>[Float] 3DUIScale</summary>
       public const int prefabScale = 42;
       /// <summary>[String] 月活动item关系表</summary>
       public const int monthAct = 43;
       /// <summary>[Int] 物品默认悬挂点</summary>
       public const int suspendPoint = 44;
   }
   public sealed class itemSp
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 物品描述</summary>
       public const int description = 2;
       /// <summary>[String] 装备类型</summary>
       public const int equ = 3;
       /// <summary>[Int] 是否可以叠加</summary>
       public const int gather = 4;
       /// <summary>[String] 图名</summary>
       public const int spriteName = 5;
       /// <summary>[String] 资源路径</summary>
       public const int prefabPath = 6;
       /// <summary>[Int] 售卖价格</summary>
       public const int money = 7;
       /// <summary>[Int] 稀有度</summary>
       public const int rare = 8;
       /// <summary>[String] 3DUI坐标</summary>
       public const int prefabPosition = 9;
       /// <summary>[String] 3DUI旋转</summary>
       public const int prefabRotation = 10;
       /// <summary>[Float] 3DUIScale</summary>
       public const int prefabScale = 11;
   }
   public sealed class itemType
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 图名</summary>
       public const int spriteName = 1;
   }
   public sealed class lvsk
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int name = 1;
       /// <summary>[Int] 大类</summary>
       public const int exp = 2;
       /// <summary>[Int] </summary>
       public const int axe = 3;
       /// <summary>[Int] </summary>
       public const int sickle = 4;
       /// <summary>[Int] </summary>
       public const int water = 5;
       /// <summary>[Int] </summary>
       public const int plant = 6;
       /// <summary>[Int] </summary>
       public const int hammer = 7;
       /// <summary>[Int] </summary>
       public const int fish = 8;
       /// <summary>[Int] </summary>
       public const int cage = 9;
   }
   public sealed class machineFood
   {
       /// <summary>[Int] 放置物品</summary>
       public const int id = 0;
       /// <summary>[Int] 需要哪种加工机</summary>
       public const int machineId = 1;
       /// <summary>[Int] 产出物品</summary>
       public const int itemid = 2;
   }
   public sealed class machineFood2
   {
       /// <summary>[Int] 放置物品</summary>
       public const int id = 0;
       /// <summary>[Int] 需要哪种加工机</summary>
       public const int machineId = 1;
       /// <summary>[Int] 产出物品</summary>
       public const int itemid = 2;
   }
   public sealed class manorRule
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名称</summary>
       public const int name = 1;
       /// <summary>[String] 图标图名</summary>
       public const int spriteName = 2;
       /// <summary>[String] 地图图标图名</summary>
       public const int mapSpriteName = 3;
       /// <summary>[String] 资源路径</summary>
       public const int prefabPath = 4;
       /// <summary>[Int] 宽度</summary>
       public const int width = 5;
       /// <summary>[Int] 长度</summary>
       public const int height = 6;
       /// <summary>[String] 形状</summary>
       public const int shape = 7;
       /// <summary>[String] 创建规则</summary>
       public const int createRule = 8;
       /// <summary>[String] 销毁规则</summary>
       public const int destoryRule = 9;
       /// <summary>[String] 编辑/摆放规则</summary>
       public const int editRule = 10;
   }
   public sealed class milk
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int name = 1;
       /// <summary>[Int] </summary>
       public const int friend = 2;
       /// <summary>[Int] </summary>
       public const int friend2 = 3;
       /// <summary>[Int] 普通牛产出</summary>
       public const int milk0 = 4;
       /// <summary>[Int] 普通羊产出</summary>
       public const int hair = 5;
       /// <summary>[Int] 放牧普通牛加成</summary>
       public const int milk2 = 6;
       /// <summary>[Int] 放牧普通羊加成</summary>
       public const int hair2 = 7;
       /// <summary>[Int] 冠军牛产出</summary>
       public const int milk3 = 8;
       /// <summary>[Int] 冠军羊产出</summary>
       public const int hair3 = 9;
       /// <summary>[Int] 放牧冠军牛加成</summary>
       public const int milk4 = 10;
       /// <summary>[Int] 放牧冠军羊加成</summary>
       public const int hair4 = 11;
   }
   public sealed class milkPer
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 放牧时间</summary>
       public const int hour = 1;
       /// <summary>[Int] 放牧时间</summary>
       public const int hour2 = 2;
       /// <summary>[Float] </summary>
       public const int per = 3;
   }
   public sealed class monthAct
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int disp = 1;
       /// <summary>[String] </summary>
       public const int name = 2;
       /// <summary>[Int] 数量要求</summary>
       public const int count = 3;
       /// <summary>[Int] 钻石奖励</summary>
       public const int money = 4;
       /// <summary>[Int] 积分奖励</summary>
       public const int point = 5;
       /// <summary>[Int] 任务类型</summary>
       public const int type = 6;
   }
   public sealed class monthHead
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 月活动积分奖励要求</summary>
       public const int point = 1;
       /// <summary>[Int] 月活动积分奖励内容</summary>
       public const int itmeid = 2;
       /// <summary>[Int] 月活动积分奖励内容数量</summary>
       public const int num = 3;
   }
   public sealed class mshopCloth
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 大类</summary>
       public const int type = 2;
       /// <summary>[Int] 对应itemID</summary>
       public const int itemid = 3;
       /// <summary>[Int] 价格</summary>
       public const int price = 4;
       /// <summary>[Int] 叠加</summary>
       public const int bag = 5;
       /// <summary>[Int] 商店类型</summary>
       public const int shoptype = 6;
       /// <summary>[Int] 去向</summary>
       public const int desTo = 7;
       /// <summary>[Int] 重复购买</summary>
       public const int repeatbuy = 8;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 9;
       /// <summary>[String] 介绍</summary>
       public const int information = 10;
       /// <summary>[Int] 季节</summary>
       public const int x = 11;
   }
   public sealed class mshopCloth2
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 大类</summary>
       public const int type = 2;
       /// <summary>[Int] 对应itemID</summary>
       public const int itemid = 3;
       /// <summary>[Int] 价格</summary>
       public const int price = 4;
       /// <summary>[Int] 叠加</summary>
       public const int bag = 5;
       /// <summary>[Int] 商店类型</summary>
       public const int shoptype = 6;
       /// <summary>[Int] 去向</summary>
       public const int desTo = 7;
       /// <summary>[Int] 重复购买</summary>
       public const int repeatbuy = 8;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 9;
       /// <summary>[String] 介绍</summary>
       public const int information = 10;
       /// <summary>[Int] 季节</summary>
       public const int x = 11;
   }
   public sealed class name
   {
       /// <summary>[Int] 格子ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int name0 = 1;
       /// <summary>[String] </summary>
       public const int name2 = 2;
       /// <summary>[String] </summary>
       public const int name3 = 3;
       /// <summary>[String] </summary>
       public const int test = 4;
   }
   public sealed class nameGirl
   {
       /// <summary>[Int] 格子ID</summary>
       public const int id = 0;
       /// <summary>[String] </summary>
       public const int name = 1;
       /// <summary>[String] </summary>
       public const int name2 = 2;
       /// <summary>[String] </summary>
       public const int name3 = 3;
       /// <summary>[String] </summary>
       public const int test = 4;
   }
   public sealed class npc
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 职位</summary>
       public const int job = 1;
       /// <summary>[String] 名字</summary>
       public const int name = 2;
       /// <summary>[Int] 是否参与委托</summary>
       public const int task = 3;
       /// <summary>[Int] 生日月</summary>
       public const int month = 4;
       /// <summary>[Int] 生日日</summary>
       public const int day = 5;
       /// <summary>[Float] 走路速度</summary>
       public const int walkSpeed = 6;
       /// <summary>[String] npc预设体路径</summary>
       public const int prefab = 7;
       /// <summary>[String] 碰撞体位置</summary>
       public const int coliCenter = 8;
       /// <summary>[String] 碰撞体大小</summary>
       public const int coliSize = 9;
       /// <summary>[String] 圆头像名称</summary>
       public const int headCircleName = 10;
       /// <summary>[String] 方头像名称</summary>
       public const int headSquareName = 11;
       /// <summary>[String] 立绘名称</summary>
       public const int paintName = 12;
       /// <summary>[String] 立绘位置</summary>
       public const int paintPos = 13;
   }
   public sealed class npcGift
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 好厌恶</summary>
       public const int likeList = 1;
       /// <summary>[Int] 是否出现在当前委托系统中</summary>
       public const int taskItem = 2;
       /// <summary>[String] 该物品出现在哪些星级委托中</summary>
       public const int taskLvNum = 3;
   }
   public sealed class objPath
   {
       /// <summary>[Int] id</summary>
       public const int id = 0;
       /// <summary>[String] 实例名字</summary>
       public const int name = 1;
       /// <summary>[String] 预制体路径</summary>
       public const int path = 2;
   }
   public sealed class OreHoleSize
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 大小</summary>
       public const int size = 2;
       /// <summary>[Int] 地下湖 0没有1有</summary>
       public const int lake = 3;
   }
   public sealed class playerPlane
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 飞机id</summary>
       public const int planeID = 1;
   }
   public sealed class plotPath
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 场景ID</summary>
       public const int sceneid = 1;
       /// <summary>[String] 行走路径</summary>
       public const int path = 2;
   }
   public sealed class que
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 委托人</summary>
       public const int npcid = 1;
       /// <summary>[Int] 季节</summary>
       public const int season = 2;
       /// <summary>[Int] 触发季节</summary>
       public const int triSeason = 3;
       /// <summary>[Int] 触发日期</summary>
       public const int triDay = 4;
       /// <summary>[Int] 委托等级</summary>
       public const int level = 5;
       /// <summary>[String] 委托出现的年限</summary>
       public const int yearLimit = 6;
       /// <summary>[String] 委托物品</summary>
       public const int itemidDict = 7;
       /// <summary>[Int] 委托物数量</summary>
       public const int num = 8;
       /// <summary>[Int] 委托对话</summary>
       public const int talkid = 9;
       /// <summary>[Int] 委托金币奖励</summary>
       public const int gold = 10;
       /// <summary>[Int] 委托经验奖励</summary>
       public const int expPasture = 11;
   }
   public sealed class queItem
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 类名字</summary>
       public const int name = 1;
       /// <summary>[Int] 开放控制</summary>
       public const int contact = 2;
       /// <summary>[String] 该类型下的泛指内容</summary>
       public const int itemidDict = 3;
   }
   public sealed class scale3DModel
   {
       /// <summary>[Int] 字典id</summary>
       public const int id = 0;
       /// <summary>[Float] X缩放</summary>
       public const int scaleX = 1;
       /// <summary>[Float] Y缩放</summary>
       public const int scaleY = 2;
       /// <summary>[Float] Z缩放</summary>
       public const int scaleZ = 3;
       /// <summary>[Float] X坐标</summary>
       public const int positionX = 4;
       /// <summary>[Float] Y坐标</summary>
       public const int positionY = 5;
       /// <summary>[Float] Z坐标</summary>
       public const int positionZ = 6;
       /// <summary>[Float] 四元数X</summary>
       public const int quaterX = 7;
       /// <summary>[Float] 四元数Y</summary>
       public const int quaterY = 8;
       /// <summary>[Float] 四元数Z</summary>
       public const int quaterZ = 9;
       /// <summary>[Float] 四元数W</summary>
       public const int quaterW = 10;
   }
   public sealed class scene
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 通知</summary>
       public const int inform = 2;
       /// <summary>[Int] 是谁的small</summary>
       public const int smallScene = 3;
       /// <summary>[Int] 时间不动</summary>
       public const int stopTime = 4;
       /// <summary>[Float] 角色出生</summary>
       public const int posx = 5;
       /// <summary>[Float] 角色出生</summary>
       public const int posy = 6;
       /// <summary>[Float] 角色出生</summary>
       public const int posz = 7;
       /// <summary>[String] npc</summary>
       public const int npcList = 8;
       /// <summary>[String] npc出生位置</summary>
       public const int npcPosList = 9;
       /// <summary>[String] npc出生方向</summary>
       public const int npcDirList = 10;
       /// <summary>[String] 鱼塘坐标</summary>
       public const int fishPondPos = 11;
       /// <summary>[String] 鱼塘水面高度</summary>
       public const int fishPondHeight = 12;
       /// <summary>[String] 鱼塘旋转</summary>
       public const int fishPondAngle = 13;
       /// <summary>[Int] 鱼的数量</summary>
       public const int fishFlushNum = 14;
       /// <summary>[String] </summary>
       public const int riverOrSea = 15;
       /// <summary>[Float] 挖矿区域坐标</summary>
       public const int oreAreaX = 16;
       /// <summary>[Float] 挖矿区域坐标</summary>
       public const int oreAreaY = 17;
       /// <summary>[Int] 行数</summary>
       public const int oreAreaCol = 18;
       /// <summary>[Int] 列数</summary>
       public const int oreAreaRow = 19;
       /// <summary>[String] 矿等于物品id</summary>
       public const int oreidList = 20;
       /// <summary>[Float] 矿区旋转</summary>
       public const int angleOre = 21;
       /// <summary>[Int] 梯子数量</summary>
       public const int ladder = 22;
       /// <summary>[Int] 梯子走向</summary>
       public const int ladderScene = 23;
       /// <summary>[Int] 刷矿上限</summary>
       public const int oreMin = 24;
       /// <summary>[Int] 刷矿上限</summary>
       public const int oreMax = 25;
       /// <summary>[String] 场景里的动物</summary>
       public const int animalList = 26;
       /// <summary>[String] 场景里动物位置</summary>
       public const int animalPosList = 27;
       /// <summary>[String] 鸟能飞的位置</summary>
       public const int birdPos = 28;
       /// <summary>[String] 动物能走的区域</summary>
       public const int animalArea = 29;
       /// <summary>[String] 牧场坐标</summary>
       public const int pasturePos = 30;
       /// <summary>[Int] 牧场列</summary>
       public const int pastureRow = 31;
       /// <summary>[Int] 牧场行</summary>
       public const int pastureCol = 32;
       /// <summary>[Float] 牧场旋转</summary>
       public const int pastureAngle = 33;
       /// <summary>[Int] 是否是烘焙的场景</summary>
       public const int isBackScene = 34;
       /// <summary>[Int] 状态数量</summary>
       public const int bakeStayCount = 35;
       /// <summary>[Int] 烘焙贴图数量</summary>
       public const int lightMapListCount = 36;
       /// <summary>[Int] 是否包含ShadowMask</summary>
       public const int isHaveShadowMask = 37;
       /// <summary>[String] 烘焙状态对应的烘焙贴图index</summary>
       public const int bakeStayToLightMapCount = 38;
       /// <summary>[Int] 光照探针数量</summary>
       public const int LightProbeCount = 39;
       /// <summary>[String] 烘焙状态对应的光照探针index</summary>
       public const int bakeStayToLightProbeCount = 40;
       /// <summary>[Int] 反射探头状态数量</summary>
       public const int reflectionProbeCount = 41;
       /// <summary>[String] 烘焙状态对应的反射探头index</summary>
       public const int bakeStayToreflectionProbeCount = 42;
   }
   public sealed class sceneAreaPos
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 随机区域</summary>
       public const int randomArea = 2;
       /// <summary>[String] 内部范围</summary>
       public const int internalArea = 3;
       /// <summary>[String] 动物购买和出生时候的位置</summary>
       public const int defaultArea = 4;
   }
   public sealed class sceneObj
   {
       /// <summary>[Int] 字典id</summary>
       public const int id = 0;
       /// <summary>[Int] 场景id</summary>
       public const int sceneid = 1;
       /// <summary>[String] name</summary>
       public const int name = 2;
       /// <summary>[Int] 类型</summary>
       public const int type = 3;
       /// <summary>[String] 预制体路径</summary>
       public const int path = 4;
       /// <summary>[Int] 显示状态</summary>
       public const int display = 5;
       /// <summary>[Int] 加载等级</summary>
       public const int loadPriority = 6;
       /// <summary>[Int] Grass位置</summary>
       public const int xzPos = 7;
       /// <summary>[Float] X坐标</summary>
       public const int x = 8;
       /// <summary>[Float] Y坐标</summary>
       public const int y = 9;
       /// <summary>[Float] Z坐标</summary>
       public const int z = 10;
       /// <summary>[Float] 四元数X</summary>
       public const int quaterX = 11;
       /// <summary>[Float] 四元数Y</summary>
       public const int quaterY = 12;
       /// <summary>[Float] 四元数Z</summary>
       public const int quaterZ = 13;
       /// <summary>[Float] 四元数W</summary>
       public const int quaterW = 14;
       /// <summary>[Float] X缩放</summary>
       public const int scaleX = 15;
       /// <summary>[Float] Y缩放</summary>
       public const int scaleY = 16;
       /// <summary>[Float] Z缩放</summary>
       public const int scaleZ = 17;
       /// <summary>[Int] layer</summary>
       public const int layer = 18;
       /// <summary>[Int] box当lightType</summary>
       public const int box = 19;
       /// <summary>[Int] triger当shadow</summary>
       public const int triger = 20;
       /// <summary>[Int] 是否是convex</summary>
       public const int convex = 21;
       /// <summary>[Int] rigi当cull</summary>
       public const int rigi = 22;
       /// <summary>[Float] boxX当r</summary>
       public const int boxX = 23;
       /// <summary>[Float] boxY当g</summary>
       public const int boxY = 24;
       /// <summary>[Float] boxZ当b</summary>
       public const int boxZ = 25;
       /// <summary>[Float] boxCenterX</summary>
       public const int boxCenterX = 26;
       /// <summary>[Float] boxCenterY</summary>
       public const int boxCenterY = 27;
       /// <summary>[Float] boxCenterZ</summary>
       public const int boxCenterZ = 28;
       /// <summary>[Int] coliType当mode</summary>
       public const int coliType = 29;
       /// <summary>[Int] coliValue</summary>
       public const int coliValue = 30;
       /// <summary>[Int] coliCondition</summary>
       public const int coliCondition = 31;
       /// <summary>[Float] nvX当intensity</summary>
       public const int nvX = 32;
       /// <summary>[Float] nvY</summary>
       public const int nvY = 33;
       /// <summary>[Float] nvZ</summary>
       public const int nvZ = 34;
       /// <summary>[Int] lod</summary>
       public const int lod = 35;
       /// <summary>[Int] 场景等级标记</summary>
       public const int sceneLV = 36;
       /// <summary>[Int] 光照贴图Index</summary>
       public const int lightmapIndex = 37;
       /// <summary>[Float] 光照贴图偏移X</summary>
       public const int lightmapScaleOffsetX = 38;
       /// <summary>[Float] 光照贴图偏移Y</summary>
       public const int lightmapScaleOffsetY = 39;
       /// <summary>[Float] 光照贴图偏移Z</summary>
       public const int lightmapScaleOffsetZ = 40;
       /// <summary>[Float] 光照贴图偏移W</summary>
       public const int lightmapScaleOffsetW = 41;
       /// <summary>[String] 灯光路径动画</summary>
       public const int lightAniPath = 42;
       /// <summary>[Int] 物品的类型</summary>
       public const int itemType = 43;
   }
   public sealed class sceneStaticObj
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 场景ID</summary>
       public const int sceneID = 2;
       /// <summary>[String] 预设体路径</summary>
       public const int path = 3;
       /// <summary>[Float] 位置X</summary>
       public const int posX = 4;
       /// <summary>[Float] 位置Y</summary>
       public const int posY = 5;
       /// <summary>[Float] 位置Z</summary>
       public const int posZ = 6;
       /// <summary>[Float] 旋转X</summary>
       public const int RotX = 7;
       /// <summary>[Float] 旋转Y</summary>
       public const int RotY = 8;
       /// <summary>[Float] 旋转Z</summary>
       public const int RotZ = 9;
       /// <summary>[Int] 碰撞体类型</summary>
       public const int coliTrigger = 10;
       /// <summary>[Float] 碰撞体中心X</summary>
       public const int coliCenterX = 11;
       /// <summary>[Float] 碰撞体中心Y</summary>
       public const int coliCenterY = 12;
       /// <summary>[Float] 碰撞体中心Z</summary>
       public const int coliCenterZ = 13;
       /// <summary>[Float] 碰撞体大小X</summary>
       public const int coliSizeX = 14;
       /// <summary>[Float] 碰撞体大小Y</summary>
       public const int coliSizeY = 15;
       /// <summary>[Float] 碰撞体大小Z</summary>
       public const int coliSizeZ = 16;
   }
   public sealed class shop
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 大类</summary>
       public const int type = 2;
       /// <summary>[Int] 对应itemID</summary>
       public const int itemid = 3;
       /// <summary>[Int] 价格</summary>
       public const int price = 4;
       /// <summary>[Int] 叠加</summary>
       public const int bag = 5;
       /// <summary>[Int] 商店类型</summary>
       public const int shoptype = 6;
       /// <summary>[Int] 去向</summary>
       public const int desTo = 7;
       /// <summary>[Int] 重复购买</summary>
       public const int repeatbuy = 8;
       /// <summary>[Int] 购买倍率</summary>
       public const int buyPower = 9;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 10;
       /// <summary>[String] 介绍</summary>
       public const int information = 11;
       /// <summary>[Int] 季节</summary>
       public const int x = 12;
   }
   public sealed class shopAnimal
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 动物类型</summary>
       public const int cosAnimalExcel = 2;
       /// <summary>[Int] 大类</summary>
       public const int type = 3;
       /// <summary>[Int] 对应itemID</summary>
       public const int itemid = 4;
       /// <summary>[Int] 购买价格</summary>
       public const int price = 5;
       /// <summary>[Float] 模型缩放</summary>
       public const int modelScale = 6;
       /// <summary>[Int] 叠加</summary>
       public const int bag = 7;
       /// <summary>[Int] 商店类型</summary>
       public const int shoptype = 8;
       /// <summary>[Int] 去向</summary>
       public const int desTo = 9;
       /// <summary>[Int] 重复购买</summary>
       public const int repeatbuy = 10;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 11;
       /// <summary>[String] 介绍</summary>
       public const int information = 12;
       /// <summary>[Int] 季节</summary>
       public const int x = 13;
   }
   public sealed class shopCup
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 价格</summary>
       public const int moneyList = 1;
       /// <summary>[String] </summary>
       public const int goldList = 2;
   }
   public sealed class shopDiy
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 对应的物品在itemsp表的编号</summary>
       public const int itemspid = 1;
       /// <summary>[Int] 是否是官方的</summary>
       public const int diy = 2;
       /// <summary>[String] 各次价格</summary>
       public const int count2moneyList = 3;
       /// <summary>[Int] 官方钻石价格</summary>
       public const int price = 4;
       /// <summary>[String] 帽子位置偏移</summary>
       public const int pos = 5;
       /// <summary>[String] 帽子大小缩放</summary>
       public const int scale = 6;
       /// <summary>[String] 帽子旋转</summary>
       public const int rot = 7;
       /// <summary>[String] 帽子的纹理贴图路径</summary>
       public const int texPath = 8;
       /// <summary>[Float] 最大范围边界偏移</summary>
       public const int maxOffset = 9;
       /// <summary>[String] 遮罩路径</summary>
       public const int maskPath = 10;
       /// <summary>[String] 遮罩名称</summary>
       public const int maskName = 11;
       /// <summary>[String] 预览根节点位置</summary>
       public const int previewRootPos = 12;
       /// <summary>[String] 预览界面位置偏移</summary>
       public const int previewPos = 13;
       /// <summary>[String] 预览界面缩放</summary>
       public const int previewScale = 14;
       /// <summary>[String] 预览界面旋转</summary>
       public const int previewRot = 15;
       /// <summary>[String] 画帽子界面帽子位置</summary>
       public const int coustomPos = 16;
       /// <summary>[String] 画帽子界面帽子缩放</summary>
       public const int coustomScale = 17;
       /// <summary>[String] 画帽子界面父物体位置</summary>
       public const int coustomParentPos = 18;
       /// <summary>[String] 画帽子界面帽子X旋转1阶段</summary>
       public const int coustomHatRotX_1 = 19;
       /// <summary>[String] 画帽子界面帽子X旋转2阶段</summary>
       public const int coustomHatRotX_2 = 20;
       /// <summary>[String] 画帽子界面帽子X旋转3阶段</summary>
       public const int coustomHatRotX_3 = 21;
       /// <summary>[String] 画帽子界面帽子X旋转4阶段</summary>
       public const int coustomHatRotX_4 = 22;
       /// <summary>[String] 画帽子界面帽子Y旋转1阶段</summary>
       public const int coustomHatRotY_1 = 23;
       /// <summary>[String] 画帽子界面帽子Y旋转2阶段</summary>
       public const int coustomHatRotY_2 = 24;
       /// <summary>[String] 画帽子界面帽子Y旋转3阶段</summary>
       public const int coustomHatRotY_3 = 25;
       /// <summary>[String] 画帽子界面帽子Y旋转4阶段</summary>
       public const int coustomHatRotY_4 = 26;
       /// <summary>[String] 帽子戴头上的相对位置</summary>
       public const int hatToHend = 27;
   }
   public sealed class shopEq
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 商店类型</summary>
       public const int type = 2;
       /// <summary>[Int] 生成新物品</summary>
       public const int itemid = 3;
       /// <summary>[String] 需要的装备类型</summary>
       public const int srcList = 4;
       /// <summary>[Int] 价格</summary>
       public const int price = 5;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 6;
       /// <summary>[Int] 需要的物品A</summary>
       public const int needItemid = 7;
       /// <summary>[Int] 需要A的数量</summary>
       public const int needNum = 8;
       /// <summary>[Int] 需要的物品B</summary>
       public const int needItemid2 = 9;
       /// <summary>[Int] 需要的B的数量</summary>
       public const int needNum2 = 10;
       /// <summary>[Int] 木头</summary>
       public const int wood = 11;
       /// <summary>[Int] 熟练度要求</summary>
       public const int proplv = 12;
       /// <summary>[String] 介绍</summary>
       public const int information = 13;
   }
   public sealed class shopFeed
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 对应itemID</summary>
       public const int itemid = 2;
       /// <summary>[Int] 价格</summary>
       public const int price = 3;
       /// <summary>[Int] 商店类型</summary>
       public const int shoptype = 4;
       /// <summary>[Int] 去向</summary>
       public const int desTo = 5;
       /// <summary>[Int] 重复购买</summary>
       public const int repeatbuy = 6;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 7;
   }
   public sealed class shopHouse
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[String] 室内图</summary>
       public const int indoorSprite = 2;
       /// <summary>[Int] 升级类型</summary>
       public const int type = 3;
       /// <summary>[Int] 升级等级</summary>
       public const int lv = 4;
       /// <summary>[Int] 矿石</summary>
       public const int itemid = 5;
       /// <summary>[Int] </summary>
       public const int num = 6;
       /// <summary>[Int] 矿石</summary>
       public const int itemid2 = 7;
       /// <summary>[Int] </summary>
       public const int num2 = 8;
       /// <summary>[Int] 图纸</summary>
       public const int itemid3 = 9;
       /// <summary>[Int] </summary>
       public const int gold = 10;
       /// <summary>[Int] </summary>
       public const int wood = 11;
       /// <summary>[String] 描述内容</summary>
       public const int info = 12;
   }
   public sealed class shopHouseskin
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 升级类型</summary>
       public const int type = 2;
       /// <summary>[Int] 矿石</summary>
       public const int itemid = 3;
       /// <summary>[Int] </summary>
       public const int num = 4;
       /// <summary>[Int] 矿石</summary>
       public const int itemid2 = 5;
       /// <summary>[Int] </summary>
       public const int num2 = 6;
       /// <summary>[Int] 图纸</summary>
       public const int itemid3 = 7;
       /// <summary>[Int] </summary>
       public const int gold = 8;
       /// <summary>[Int] </summary>
       public const int wood = 9;
   }
   public sealed class shopMachine
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 商店类型</summary>
       public const int type = 1;
       /// <summary>[Int] 所属场景</summary>
       public const int sceneid = 2;
       /// <summary>[Int] 购买条件(等级)</summary>
       public const int sceneLv = 3;
       /// <summary>[String] 图标</summary>
       public const int sprName = 4;
       /// <summary>[Int] 价格</summary>
       public const int price = 5;
       /// <summary>[Int] 钻石金币</summary>
       public const int consume = 6;
       /// <summary>[Int] 需要的物品A</summary>
       public const int needItemid = 7;
       /// <summary>[Int] 需要A的数量</summary>
       public const int needNum = 8;
       /// <summary>[Int] 需要的物品B</summary>
       public const int needItemid2 = 9;
       /// <summary>[Int] 需要的B的数量</summary>
       public const int needNum2 = 10;
   }
   public sealed class shopRest
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 营业开始时间</summary>
       public const int busyhour = 1;
       /// <summary>[Int] 营业结束时间</summary>
       public const int endhour = 2;
       /// <summary>[Int] 星期几休息</summary>
       public const int weekday = 3;
   }
   public sealed class sign
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] </summary>
       public const int itemid = 1;
       /// <summary>[Int] </summary>
       public const int gold = 2;
       /// <summary>[Int] </summary>
       public const int money = 3;
       /// <summary>[Int] </summary>
       public const int wood = 4;
   }
   public sealed class sign7
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] </summary>
       public const int itemid = 1;
       /// <summary>[Int] </summary>
       public const int gold = 2;
       /// <summary>[Int] </summary>
       public const int money = 3;
       /// <summary>[Int] </summary>
       public const int wood = 4;
   }
   public sealed class signact
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 签到天数</summary>
       public const int dayNum = 1;
       /// <summary>[Int] 活动持续天数</summary>
       public const int keepNum = 2;
       /// <summary>[String] 奖励</summary>
       public const int rewardList = 3;
       /// <summary>[String] 奖励数量</summary>
       public const int numList = 4;
   }
   public sealed class sprite
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] 生日月份</summary>
       public const int month = 2;
       /// <summary>[Int] 日子</summary>
       public const int day = 3;
       /// <summary>[String] 小精灵遮罩颜色A</summary>
       public const int colorA = 4;
       /// <summary>[String] 小精灵遮罩颜色B</summary>
       public const int colorB = 5;
       /// <summary>[String] 小精灵遮罩颜色C</summary>
       public const int colorC = 6;
       /// <summary>[String] 对话立绘位置</summary>
       public const int paintPos = 7;
       /// <summary>[Float] 碰撞体大小</summary>
       public const int coliSizeX = 8;
       /// <summary>[Float] </summary>
       public const int coliSizeY = 9;
       /// <summary>[Float] </summary>
       public const int coliSizeZ = 10;
       /// <summary>[Float] 碰撞体偏移</summary>
       public const int coliOffsetX = 11;
       /// <summary>[Float] </summary>
       public const int coliOffsetY = 12;
       /// <summary>[Float] </summary>
       public const int coliOffsetZ = 13;
       /// <summary>[String] 路径</summary>
       public const int objPath = 14;
       /// <summary>[String] 材质主贴图</summary>
       public const int mainTex = 15;
       /// <summary>[String] 材质遮罩图</summary>
       public const int maskTex = 16;
       /// <summary>[String] 动画状态机</summary>
       public const int animator = 17;
   }
   public sealed class spriteGift
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 好厌恶</summary>
       public const int likeList = 1;
   }
   public sealed class spriteLv
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 浇水等级</summary>
       public const int waterLV = 1;
       /// <summary>[Int] 浇水需要经验</summary>
       public const int waterExp = 2;
       /// <summary>[Int] 浇水能力</summary>
       public const int waterAbility = 3;
       /// <summary>[Int] 耕种等级</summary>
       public const int landLv = 4;
       /// <summary>[Int] 耕种需要经验</summary>
       public const int landExp = 5;
       /// <summary>[Int] 耕地能力</summary>
       public const int landAbility = 6;
       /// <summary>[Int] 照顾动物等级</summary>
       public const int animalLV = 7;
       /// <summary>[Int] 照顾动物需要经验</summary>
       public const int animalExp = 8;
       /// <summary>[Int] 照顾动物能力</summary>
       public const int animalAbility = 9;
       /// <summary>[Float] 移动速度倍率</summary>
       public const int moveSped = 10;
       /// <summary>[Float] 施法时间倍率</summary>
       public const int duraTime = 11;
       /// <summary>[Int] 爱心数量</summary>
       public const int loveNumber = 12;
       /// <summary>[Float] 爱心能力加成</summary>
       public const int loveImprove = 13;
       /// <summary>[Int] 爱心范围经验上</summary>
       public const int loveDown = 14;
       /// <summary>[Int] 爱心范围经验下</summary>
       public const int loveUp = 15;
       /// <summary>[String] 精灵名字</summary>
       public const int name = 16;
       /// <summary>[String] 精灵图片名字</summary>
       public const int spriteName = 17;
       /// <summary>[Int] 浇水最大等级</summary>
       public const int waterMaxLv = 18;
       /// <summary>[Int] 收获最大等级</summary>
       public const int harvestMaxLv = 19;
       /// <summary>[Int] 照顾动物最大等级</summary>
       public const int animalMaxLv = 20;
   }
   public sealed class spriteScene
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Float] 角色出生</summary>
       public const int posx = 2;
       /// <summary>[Float] 角色出生</summary>
       public const int posy = 3;
       /// <summary>[Float] 角色出生</summary>
       public const int posz = 4;
   }
   public sealed class stone
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int name = 1;
       /// <summary>[Int] </summary>
       public const int type = 2;
       /// <summary>[Int] 血量</summary>
       public const int Hp = 3;
       /// <summary>[Int] 叠加</summary>
       public const int impose = 4;
       /// <summary>[Float] 速度</summary>
       public const int speed = 5;
       /// <summary>[String] </summary>
       public const int prefab = 6;
   }
   public sealed class story
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名称</summary>
       public const int name = 1;
       /// <summary>[Int] 讲述时间</summary>
       public const int week = 2;
       /// <summary>[Int] 引用的itemID</summary>
       public const int itemID = 3;
   }
   public sealed class sugar
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 金币||钻石</summary>
       public const int consume = 1;
       /// <summary>[Int] 价值</summary>
       public const int price = 2;
       /// <summary>[Int] 当前购买次数</summary>
       public const int count = 3;
       /// <summary>[Int] vip等级</summary>
       public const int vip = 4;
       /// <summary>[Int] 购买上限</summary>
       public const int max = 5;
   }
   public sealed class talk
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 对话的人</summary>
       public const int person = 1;
       /// <summary>[Int] 心情</summary>
       public const int mood = 2;
   }
   public sealed class taskLv
   {
       /// <summary>[Int] 牧场等级</summary>
       public const int id = 0;
       /// <summary>[Int] 1星委托概率</summary>
       public const int lv1 = 1;
       /// <summary>[Int] 2星委托概率</summary>
       public const int lv2 = 2;
       /// <summary>[Int] 3星委托概率</summary>
       public const int lv3 = 3;
       /// <summary>[Int] 4星委托概率</summary>
       public const int lv4 = 4;
       /// <summary>[Int] 5星委托概率</summary>
       public const int lv5 = 5;
       /// <summary>[Int] 6星委托概率</summary>
       public const int lv6 = 6;
   }
   public sealed class taskReward
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 金币奖励</summary>
       public const int coin = 1;
       /// <summary>[String] 牧场经验奖励</summary>
       public const int pas = 2;
       /// <summary>[String] 图纸积分奖励</summary>
       public const int paper = 3;
       /// <summary>[Int] 钻石完成花费</summary>
       public const int money = 4;
   }
   public sealed class temperature
   {
       /// <summary>[Int] 格子ID</summary>
       public const int id = 0;
       /// <summary>[Int] 最低气温</summary>
       public const int minTemp = 1;
       /// <summary>[Int] 最高气温</summary>
       public const int maxTemp = 2;
       /// <summary>[Int] 春</summary>
       public const int spring = 3;
       /// <summary>[Int] 夏</summary>
       public const int summer = 4;
       /// <summary>[Int] 秋</summary>
       public const int autumn = 5;
       /// <summary>[Int] 冬</summary>
       public const int winter = 6;
   }
   public sealed class testCropDebuger
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 农作物行1</summary>
       public const int crop1 = 1;
       /// <summary>[String] 农作物行2</summary>
       public const int crop2 = 2;
       /// <summary>[String] 农作物行3</summary>
       public const int crop3 = 3;
       /// <summary>[String] 农作物行4</summary>
       public const int crop4 = 4;
       /// <summary>[String] 农作物行5</summary>
       public const int crop5 = 5;
       /// <summary>[String] 农作物行6</summary>
       public const int crop6 = 6;
       /// <summary>[String] 农作物行7</summary>
       public const int crop7 = 7;
       /// <summary>[String] 农作物行8</summary>
       public const int crop8 = 8;
       /// <summary>[String] 农作物行9</summary>
       public const int crop9 = 9;
       /// <summary>[String] 农作物行10</summary>
       public const int crop10 = 10;
       /// <summary>[String] 农作物行11</summary>
       public const int crop11 = 11;
       /// <summary>[String] 农作物行12</summary>
       public const int crop12 = 12;
       /// <summary>[String] 农作物行13</summary>
       public const int crop13 = 13;
       /// <summary>[String] 农作物行14</summary>
       public const int crop14 = 14;
       /// <summary>[String] 农作物行15</summary>
       public const int crop15 = 15;
       /// <summary>[String] 农作物行16</summary>
       public const int crop16 = 16;
       /// <summary>[String] 农作物行17</summary>
       public const int crop17 = 17;
       /// <summary>[String] 农作物行18</summary>
       public const int crop18 = 18;
       /// <summary>[String] 农作物行19</summary>
       public const int crop19 = 19;
       /// <summary>[String] 农作物行20</summary>
       public const int crop20 = 20;
       /// <summary>[String] 农作物行21</summary>
       public const int crop21 = 21;
       /// <summary>[String] 农作物行22</summary>
       public const int crop22 = 22;
       /// <summary>[String] 农作物行23</summary>
       public const int crop23 = 23;
       /// <summary>[String] 农作物行24</summary>
       public const int crop24 = 24;
       /// <summary>[String] 农作物行25</summary>
       public const int crop25 = 25;
       /// <summary>[String] 农作物行26</summary>
       public const int crop26 = 26;
       /// <summary>[String] 农作物行27</summary>
       public const int crop27 = 27;
       /// <summary>[String] 农作物行28</summary>
       public const int crop28 = 28;
       /// <summary>[String] 农作物行29</summary>
       public const int crop29 = 29;
       /// <summary>[String] 农作物行30</summary>
       public const int crop30 = 30;
       /// <summary>[String] 农作物行31</summary>
       public const int crop31 = 31;
       /// <summary>[String] 农作物行32</summary>
       public const int crop32 = 32;
       /// <summary>[String] 农作物行33</summary>
       public const int crop33 = 33;
       /// <summary>[String] 农作物行34</summary>
       public const int crop34 = 34;
       /// <summary>[String] 农作物行35</summary>
       public const int crop35 = 35;
       /// <summary>[String] 农作物行36</summary>
       public const int crop36 = 36;
       /// <summary>[String] 农作物行37</summary>
       public const int crop37 = 37;
       /// <summary>[String] 农作物行38</summary>
       public const int crop38 = 38;
       /// <summary>[String] 农作物行39</summary>
       public const int crop39 = 39;
       /// <summary>[String] 农作物行40</summary>
       public const int crop40 = 40;
       /// <summary>[String] 农作物行41</summary>
       public const int crop41 = 41;
       /// <summary>[String] 农作物行42</summary>
       public const int crop42 = 42;
       /// <summary>[String] 农作物行43</summary>
       public const int crop43 = 43;
       /// <summary>[String] 农作物行44</summary>
       public const int crop44 = 44;
       /// <summary>[String] 农作物行45</summary>
       public const int crop45 = 45;
   }
   public sealed class uiConfig
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 山货每日随机数</summary>
       public const int forestProductsResetNum = 1;
       /// <summary>[Int] 水产每日随机数</summary>
       public const int aquaticResetNum = 2;
       /// <summary>[Int] 菜肴每日随机数</summary>
       public const int dishesResetNum = 3;
       /// <summary>[Int] 蔬菜种子每日随机数</summary>
       public const int vegSeedResetNum = 4;
   }
   public sealed class valueConst
   {
       /// <summary>[Int] </summary>
       public const int id = 0;
       /// <summary>[Int] 象牙恢复时间真小时</summary>
       public const int ivaryHour = 1;
       /// <summary>[Int] 周活动积分兑换上限</summary>
       public const int weekPointLimite = 2;
       /// <summary>[Int] 积分种子购买价格</summary>
       public const int weekPointSeedPrice = 3;
       /// <summary>[Int] 积分购买价格</summary>
       public const int weekPointPrice = 4;
       /// <summary>[Int] 月卡购买后持续获得钻石天数</summary>
       public const int daynum = 5;
       /// <summary>[Int] 月卡持续的每天获得钻石数量</summary>
       public const int moneynum = 6;
       /// <summary>[Int] 鸡蛋消失天数</summary>
       public const int eggDisDay = 7;
       /// <summary>[Int] 锻造石价格</summary>
       public const int equipPointPrice = 8;
       /// <summary>[Int] sp物品上限</summary>
       public const int itemspMax = 9;
       /// <summary>[Int] 每日可以给其他人点赞的次数</summary>
       public const int praiseLimite = 10;
       /// <summary>[Int] 钻石交易市场下架天数</summary>
       public const int tradeSuperDay = 11;
       /// <summary>[Int] 牛羊好感度上限</summary>
       public const int aniCowFriendMax = 12;
       /// <summary>[Int] 鸡好感度上限</summary>
       public const int aniChFriendMax = 13;
   }
   public sealed class vip
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 交易单上限</summary>
       public const int trade = 1;
       /// <summary>[Int] 冰箱上限</summary>
       public const int ice = 2;
       /// <summary>[Int] </summary>
       public const int equip = 3;
       /// <summary>[Int] </summary>
       public const int item = 4;
       /// <summary>[Int] </summary>
       public const int exp = 5;
       /// <summary>[Int] 每日委托完成数量上限</summary>
       public const int queNpcCount = 6;
   }
   public sealed class Vipexp
   {
       /// <summary>[Int] 编号</summary>
       public const int id = 0;
       /// <summary>[Int] Vip等级</summary>
       public const int viplevel = 1;
       /// <summary>[Int] 到该等级需要的总经验</summary>
       public const int vipexp = 2;
   }
   public sealed class z_ach
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 简体说明</summary>
       public const int cnreadme = 2;
       /// <summary>[String] 繁体名字</summary>
       public const int hkname = 3;
       /// <summary>[String] 繁体说明</summary>
       public const int hkreadme = 4;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 5;
       /// <summary>[String] 英文说明</summary>
       public const int enreadme = 6;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 7;
       /// <summary>[String] 法语说明</summary>
       public const int frreadme = 8;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 9;
       /// <summary>[String] 西班牙语说明</summary>
       public const int esreadme = 10;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 11;
       /// <summary>[String] 葡萄牙语说明</summary>
       public const int ptreadme = 12;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 13;
       /// <summary>[String] 德语说明</summary>
       public const int dereadme = 14;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 15;
       /// <summary>[String] 韩语说明</summary>
       public const int koreadme = 16;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 17;
       /// <summary>[String] 日语说明</summary>
       public const int jareadme = 18;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 19;
       /// <summary>[String] 俄语说明</summary>
       public const int rureadme = 20;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 21;
       /// <summary>[String] 意大利语说明</summary>
       public const int itreadme = 22;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 23;
       /// <summary>[String] 波兰语说明</summary>
       public const int plreadme = 24;
   }
   public sealed class Z_achOri
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体基础称号</summary>
       public const int cntitle = 1;
       /// <summary>[String] 繁体基础称号</summary>
       public const int hktitle = 2;
       /// <summary>[String] 英文基础称号</summary>
       public const int entitle = 3;
       /// <summary>[String] 法语基础称号</summary>
       public const int frtitle = 4;
       /// <summary>[String] 西班牙语基础称号</summary>
       public const int estitle = 5;
       /// <summary>[String] 葡萄牙语基础称号</summary>
       public const int pttitle = 6;
       /// <summary>[String] 德语基础称号</summary>
       public const int detitle = 7;
       /// <summary>[String] 韩语基础称号</summary>
       public const int kotitle = 8;
       /// <summary>[String] 日语基础称号</summary>
       public const int jatitle = 9;
       /// <summary>[String] 俄语基础称号</summary>
       public const int rutitle = 10;
       /// <summary>[String] 意大利语基础称号</summary>
       public const int ittitle = 11;
       /// <summary>[String] 波兰语基础称号</summary>
       public const int pltitle = 12;
   }
   public sealed class z_actShopRose
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体积分描述</summary>
       public const int cndisp = 1;
       /// <summary>[String] 简体活动名称</summary>
       public const int cnname = 2;
       /// <summary>[String] 繁体积分描述</summary>
       public const int hkdisp = 3;
       /// <summary>[String] 繁体活动名称</summary>
       public const int hkname = 4;
       /// <summary>[String] 英文积分描述</summary>
       public const int endisp = 5;
       /// <summary>[String] 英文活动名称</summary>
       public const int enname = 6;
       /// <summary>[String] 法语积分描述</summary>
       public const int frdisp = 7;
       /// <summary>[String] 法语活动名称</summary>
       public const int frname = 8;
       /// <summary>[String] 西班牙语积分描述</summary>
       public const int esdisp = 9;
       /// <summary>[String] 西班牙语活动名称</summary>
       public const int esname = 10;
       /// <summary>[String] 葡萄牙语积分描述</summary>
       public const int ptdisp = 11;
       /// <summary>[String] 葡萄牙语活动名称</summary>
       public const int ptname = 12;
       /// <summary>[String] 德语积分描述</summary>
       public const int dedisp = 13;
       /// <summary>[String] 德语活动名称</summary>
       public const int dename = 14;
       /// <summary>[String] 韩语积分描述</summary>
       public const int kodisp = 15;
       /// <summary>[String] 韩语活动名称</summary>
       public const int koname = 16;
       /// <summary>[String] 日语积分描述</summary>
       public const int jadisp = 17;
       /// <summary>[String] 日语活动名称</summary>
       public const int janame = 18;
       /// <summary>[String] 俄语积分描述</summary>
       public const int rudisp = 19;
       /// <summary>[String] 俄语活动名称</summary>
       public const int runame = 20;
       /// <summary>[String] 意大利语积分描述</summary>
       public const int itdisp = 21;
       /// <summary>[String] 意大利语活动名称</summary>
       public const int itname = 22;
       /// <summary>[String] 波兰语积分描述</summary>
       public const int pldisp = 23;
       /// <summary>[String] 波兰语活动名称</summary>
       public const int plname = 24;
   }
   public sealed class z_animal
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体中文动物名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁體中文动物名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文动物名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法語动物名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙語动物名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙語动物名字</summary>
       public const int ptname = 6;
       /// <summary>[String] 德語动物名字</summary>
       public const int dename = 7;
       /// <summary>[String] 韓語动物名字</summary>
       public const int koname = 8;
       /// <summary>[String] 日語动物名字</summary>
       public const int janame = 9;
       /// <summary>[String] 俄語动物名字</summary>
       public const int runame = 10;
       /// <summary>[String] 義大利語动物名字</summary>
       public const int itname = 11;
       /// <summary>[String] 波蘭語动物名字</summary>
       public const int plname = 12;
   }
   public sealed class z_buyLure
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 8;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 12;
   }
   public sealed class z_buyMoneyBargain
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体对话</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体对话</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文对话</summary>
       public const int enname = 3;
       /// <summary>[String] 法语对话</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语对话</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语对话</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语对话</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语对话</summary>
       public const int koname = 8;
       /// <summary>[String] 日语对话</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语对话</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语对话</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语对话</summary>
       public const int plname = 12;
   }
   public sealed class z_buyMoneyEquip
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体对话</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体对话</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文对话</summary>
       public const int enname = 3;
       /// <summary>[String] 法语对话</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语对话</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语对话</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语对话</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语对话</summary>
       public const int koname = 8;
       /// <summary>[String] 日语对话</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语对话</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语对话</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语对话</summary>
       public const int plname = 12;
   }
   public sealed class z_eggBox
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名称</summary>
       public const int cnname = 1;
       /// <summary>[String] 简体描述</summary>
       public const int cndisp = 2;
       /// <summary>[String] 简体扭蛋描述</summary>
       public const int cndisp1 = 3;
       /// <summary>[String] 繁体名称</summary>
       public const int hkname = 4;
       /// <summary>[String] 繁体描述</summary>
       public const int hkdisp = 5;
       /// <summary>[String] 繁体扭蛋描述</summary>
       public const int hkdisp1 = 6;
       /// <summary>[String] 英文名称</summary>
       public const int enname = 7;
       /// <summary>[String] 英文描述</summary>
       public const int endisp = 8;
       /// <summary>[String] 英文扭蛋描述</summary>
       public const int endisp1 = 9;
       /// <summary>[String] 法语名称</summary>
       public const int frname = 10;
       /// <summary>[String] 法语描述</summary>
       public const int frdisp = 11;
       /// <summary>[String] 法语扭蛋描述</summary>
       public const int frdisp1 = 12;
       /// <summary>[String] 西班牙语名称</summary>
       public const int esname = 13;
       /// <summary>[String] 西班牙语描述</summary>
       public const int esdisp = 14;
       /// <summary>[String] 西班牙语扭蛋描述</summary>
       public const int esdisp1 = 15;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 16;
       /// <summary>[String] 葡萄牙语描述</summary>
       public const int ptdisp = 17;
       /// <summary>[String] 葡萄牙语扭蛋描述</summary>
       public const int ptdisp1 = 18;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 19;
       /// <summary>[String] 德语描述</summary>
       public const int dedisp = 20;
       /// <summary>[String] 德语扭蛋描述</summary>
       public const int dedisp1 = 21;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 22;
       /// <summary>[String] 韩语描述</summary>
       public const int kodisp = 23;
       /// <summary>[String] 韩语扭蛋描述</summary>
       public const int kodisp1 = 24;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 25;
       /// <summary>[String] 日语描述</summary>
       public const int jadisp = 26;
       /// <summary>[String] 日语扭蛋描述</summary>
       public const int jadisp1 = 27;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 28;
       /// <summary>[String] 俄语描述</summary>
       public const int rudisp = 29;
       /// <summary>[String] 俄语扭蛋描述</summary>
       public const int rudisp1 = 30;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 31;
       /// <summary>[String] 意大利语描述</summary>
       public const int itdisp = 32;
       /// <summary>[String] 意大利语扭蛋描述</summary>
       public const int itdisp1 = 33;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 34;
       /// <summary>[String] 波兰语描述</summary>
       public const int pldisp = 35;
       /// <summary>[String] 波兰语扭蛋描述</summary>
       public const int pldisp1 = 36;
   }
   public sealed class z_festival
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体节日名称</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体节日名称</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文节日名称</summary>
       public const int enname = 3;
       /// <summary>[String] 法语节日名称</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语节日名称</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语节日名称</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语节日名称</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语节日名称</summary>
       public const int koname = 8;
       /// <summary>[String] 日语节日名称</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语节日名称</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语节日名称</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语节日名称</summary>
       public const int plname = 12;
   }
   public sealed class z_game
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体小游戏名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体小游戏名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文小游戏名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法语小游戏名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语小游戏名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语小游戏名称</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语小游戏名称</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语小游戏名称</summary>
       public const int koname = 8;
       /// <summary>[String] 日语小游戏名称</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语小游戏名称</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语小游戏名称</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语小游戏名称</summary>
       public const int plname = 12;
   }
   public sealed class z_item
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名称</summary>
       public const int cnname = 1;
       /// <summary>[String] 简体描述</summary>
       public const int cninformation = 2;
       /// <summary>[String] 繁体名称</summary>
       public const int hkname = 3;
       /// <summary>[String] 繁体描述</summary>
       public const int hkinformation = 4;
       /// <summary>[String] 英文名称</summary>
       public const int enname = 5;
       /// <summary>[String] 英文描述</summary>
       public const int eninformation = 6;
       /// <summary>[String] 法语名称</summary>
       public const int frname = 7;
       /// <summary>[String] 法语描述</summary>
       public const int frinformation = 8;
       /// <summary>[String] 西班牙语名称</summary>
       public const int esname = 9;
       /// <summary>[String] 西班牙语描述</summary>
       public const int esinformation = 10;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 11;
       /// <summary>[String] 葡萄牙语描述</summary>
       public const int ptinformation = 12;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 13;
       /// <summary>[String] 德语描述</summary>
       public const int deinformation = 14;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 15;
       /// <summary>[String] 韩语描述</summary>
       public const int koinformation = 16;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 17;
       /// <summary>[String] 日语描述</summary>
       public const int jainformation = 18;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 19;
       /// <summary>[String] 俄语描述</summary>
       public const int ruinformation = 20;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 21;
       /// <summary>[String] 意大利语描述</summary>
       public const int itinformation = 22;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 23;
       /// <summary>[String] 波兰语描述</summary>
       public const int plinformation = 24;
   }
   public sealed class z_itemSp
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名称</summary>
       public const int cnname = 1;
       /// <summary>[String] 简体描述</summary>
       public const int cndiscription = 2;
       /// <summary>[String] 繁体名称</summary>
       public const int hkname = 3;
       /// <summary>[String] 繁体描述</summary>
       public const int hkdiscription = 4;
       /// <summary>[String] 英文名称</summary>
       public const int enname = 5;
       /// <summary>[String] 英文描述</summary>
       public const int endiscription = 6;
       /// <summary>[String] 法语名称</summary>
       public const int frname = 7;
       /// <summary>[String] 法语描述</summary>
       public const int frdiscription = 8;
       /// <summary>[String] 西班牙语名称</summary>
       public const int esname = 9;
       /// <summary>[String] 西班牙语描述</summary>
       public const int esdiscription = 10;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 11;
       /// <summary>[String] 葡萄牙语描述</summary>
       public const int ptdiscription = 12;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 13;
       /// <summary>[String] 德语描述</summary>
       public const int dediscription = 14;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 15;
       /// <summary>[String] 韩语描述</summary>
       public const int kodiscription = 16;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 17;
       /// <summary>[String] 日语描述</summary>
       public const int jadiscription = 18;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 19;
       /// <summary>[String] 俄语描述</summary>
       public const int rudiscription = 20;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 21;
       /// <summary>[String] 意大利语描述</summary>
       public const int itdiscription = 22;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 23;
       /// <summary>[String] 波兰语描述</summary>
       public const int pldiscription = 24;
   }
   public sealed class z_itemType
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语名字</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语名字</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语名字</summary>
       public const int koname = 8;
       /// <summary>[String] 日语名字</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语名字</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语名字</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语名字</summary>
       public const int plname = 12;
   }
   public sealed class z_lang
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 簡體中文</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁體中文</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文</summary>
       public const int enname = 3;
       /// <summary>[String] 法語</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙語</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙語</summary>
       public const int ptname = 6;
       /// <summary>[String] 德語</summary>
       public const int dename = 7;
       /// <summary>[String] 韓語</summary>
       public const int koname = 8;
       /// <summary>[String] 日語</summary>
       public const int janame = 9;
       /// <summary>[String] 俄語</summary>
       public const int runame = 10;
       /// <summary>[String] 義大利語</summary>
       public const int itname = 11;
       /// <summary>[String] 波蘭語</summary>
       public const int plname = 12;
   }
   public sealed class z_mail
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体标题</summary>
       public const int cntheme = 1;
       /// <summary>[String] 简体内容</summary>
       public const int cncontent = 2;
       /// <summary>[String] 简体发件人</summary>
       public const int cnname = 3;
       /// <summary>[String] 繁体标题</summary>
       public const int hktheme = 4;
       /// <summary>[String] 繁体内容</summary>
       public const int hkcontent = 5;
       /// <summary>[String] 繁体发件人</summary>
       public const int hkname = 6;
       /// <summary>[String] 英文标题</summary>
       public const int entheme = 7;
       /// <summary>[String] 英文内容</summary>
       public const int encontent = 8;
       /// <summary>[String] 英文发件人</summary>
       public const int enname = 9;
       /// <summary>[String] 法语标题</summary>
       public const int frtheme = 10;
       /// <summary>[String] 法语内容</summary>
       public const int frcontent = 11;
       /// <summary>[String] 法语发件人</summary>
       public const int frname = 12;
       /// <summary>[String] 西班牙语标题</summary>
       public const int estheme = 13;
       /// <summary>[String] 西班牙语内容</summary>
       public const int escontent = 14;
       /// <summary>[String] 西班牙语发件人</summary>
       public const int esname = 15;
       /// <summary>[String] 葡萄牙语标题</summary>
       public const int pttheme = 16;
       /// <summary>[String] 葡萄牙语内容</summary>
       public const int ptcontent = 17;
       /// <summary>[String] 葡萄牙语发件人</summary>
       public const int ptname = 18;
       /// <summary>[String] 德语标题</summary>
       public const int detheme = 19;
       /// <summary>[String] 德语内容</summary>
       public const int decontent = 20;
       /// <summary>[String] 德语发件人</summary>
       public const int dename = 21;
       /// <summary>[String] 韩语标题</summary>
       public const int kotheme = 22;
       /// <summary>[String] 韩语内容</summary>
       public const int kocontent = 23;
       /// <summary>[String] 韩语发件人</summary>
       public const int koname = 24;
       /// <summary>[String] 日语标题</summary>
       public const int jatheme = 25;
       /// <summary>[String] 日语内容</summary>
       public const int jacontent = 26;
       /// <summary>[String] 日语发件人</summary>
       public const int janame = 27;
       /// <summary>[String] 俄语标题</summary>
       public const int rutheme = 28;
       /// <summary>[String] 俄语内容</summary>
       public const int rucontent = 29;
       /// <summary>[String] 俄语发件人</summary>
       public const int runame = 30;
       /// <summary>[String] 意大利语标题</summary>
       public const int ittheme = 31;
       /// <summary>[String] 意大利语内容</summary>
       public const int itcontent = 32;
       /// <summary>[String] 意大利语发件人</summary>
       public const int itname = 33;
       /// <summary>[String] 波兰语标题</summary>
       public const int pltheme = 34;
       /// <summary>[String] 波兰语内容</summary>
       public const int plcontent = 35;
       /// <summary>[String] 波兰语发件人</summary>
       public const int plname = 36;
   }
   public sealed class z_manorRule
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名称</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体名称</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文名称</summary>
       public const int enname = 3;
       /// <summary>[String] 法语名称</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语名称</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 8;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 12;
   }
   public sealed class z_monthAct
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体细分活动</summary>
       public const int cndisp = 1;
       /// <summary>[String] 简体活动类目</summary>
       public const int cnname = 2;
       /// <summary>[String] 繁体细分活动</summary>
       public const int hkdisp = 3;
       /// <summary>[String] 繁体活动类目</summary>
       public const int hkname = 4;
       /// <summary>[String] 英文细分活动</summary>
       public const int endisp = 5;
       /// <summary>[String] 英文活动类目</summary>
       public const int enname = 6;
       /// <summary>[String] 法语细分活动</summary>
       public const int frdisp = 7;
       /// <summary>[String] 法语活动类目</summary>
       public const int frname = 8;
       /// <summary>[String] 西班牙语细分活动</summary>
       public const int esdisp = 9;
       /// <summary>[String] 西班牙语活动类目</summary>
       public const int esname = 10;
       /// <summary>[String] 葡萄牙语细分活动</summary>
       public const int ptdisp = 11;
       /// <summary>[String] 葡萄牙语活动类目</summary>
       public const int ptname = 12;
       /// <summary>[String] 德语细分活动</summary>
       public const int dedisp = 13;
       /// <summary>[String] 德语活动类目</summary>
       public const int dename = 14;
       /// <summary>[String] 韩语细分活动</summary>
       public const int kodisp = 15;
       /// <summary>[String] 韩语活动类目</summary>
       public const int koname = 16;
       /// <summary>[String] 日语细分活动</summary>
       public const int jadisp = 17;
       /// <summary>[String] 日语活动类目</summary>
       public const int janame = 18;
       /// <summary>[String] 俄语细分活动</summary>
       public const int rudisp = 19;
       /// <summary>[String] 俄语活动类目</summary>
       public const int runame = 20;
       /// <summary>[String] 意大利语细分活动</summary>
       public const int itdisp = 21;
       /// <summary>[String] 意大利语活动类目</summary>
       public const int itname = 22;
       /// <summary>[String] 波兰语细分活动</summary>
       public const int pldisp = 23;
       /// <summary>[String] 波兰语活动类目</summary>
       public const int plname = 24;
   }
   public sealed class z_npc
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体中文职业</summary>
       public const int cnjob = 1;
       /// <summary>[String] 简体中文名字</summary>
       public const int cnname = 2;
       /// <summary>[String] 繁体中文职业</summary>
       public const int hkjob = 3;
       /// <summary>[String] 繁体中文名字</summary>
       public const int hkname = 4;
       /// <summary>[String] 英文职业</summary>
       public const int enjob = 5;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 6;
       /// <summary>[String] 法语职业</summary>
       public const int frjob = 7;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 8;
       /// <summary>[String] 西班牙语职业</summary>
       public const int esjob = 9;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 10;
       /// <summary>[String] 葡萄牙语职业</summary>
       public const int ptjob = 11;
       /// <summary>[String] 葡萄牙语名字</summary>
       public const int ptname = 12;
       /// <summary>[String] 德语职业</summary>
       public const int dejob = 13;
       /// <summary>[String] 德语名字</summary>
       public const int dename = 14;
       /// <summary>[String] 韩语职业</summary>
       public const int kojob = 15;
       /// <summary>[String] 韩语名字</summary>
       public const int koname = 16;
       /// <summary>[String] 日语职业</summary>
       public const int jajob = 17;
       /// <summary>[String] 日语名字</summary>
       public const int janame = 18;
       /// <summary>[String] 俄语职业</summary>
       public const int rujob = 19;
       /// <summary>[String] 俄语名字</summary>
       public const int runame = 20;
       /// <summary>[String] 意大利语职业</summary>
       public const int itjob = 21;
       /// <summary>[String] 意大利语名字</summary>
       public const int itname = 22;
       /// <summary>[String] 波兰语职业</summary>
       public const int pljob = 23;
       /// <summary>[String] 波兰语名字</summary>
       public const int plname = 24;
   }
   public sealed class z_OreHoleSize
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 8;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 12;
   }
   public sealed class z_scene
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体通知</summary>
       public const int cninform = 1;
       /// <summary>[String] 繁体通知</summary>
       public const int hkinform = 2;
       /// <summary>[String] 英文通知</summary>
       public const int eninform = 3;
       /// <summary>[String] 法语通知</summary>
       public const int frinform = 4;
       /// <summary>[String] 西班牙语通知</summary>
       public const int esinform = 5;
       /// <summary>[String] 葡萄牙语通知</summary>
       public const int ptinform = 6;
       /// <summary>[String] 德语通知</summary>
       public const int deinform = 7;
       /// <summary>[String] 韩语通知</summary>
       public const int koinform = 8;
       /// <summary>[String] 日语通知</summary>
       public const int jainform = 9;
       /// <summary>[String] 俄语通知</summary>
       public const int ruinform = 10;
       /// <summary>[String] 意大利语通知</summary>
       public const int itinform = 11;
       /// <summary>[String] 波兰语通知</summary>
       public const int plinform = 12;
   }
   public sealed class z_sceneStaticObj
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语名称</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语名称</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语名称</summary>
       public const int koname = 8;
       /// <summary>[String] 日语名称</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语名称</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语名称</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语名称</summary>
       public const int plname = 12;
   }
   public sealed class z_shopMachine
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体中文名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 简体中文介绍</summary>
       public const int cninformation = 2;
       /// <summary>[String] 繁体中文名字</summary>
       public const int hkname = 3;
       /// <summary>[String] 繁体中文介绍</summary>
       public const int hkinformation = 4;
       /// <summary>[String] 英文名字</summary>
       public const int enname = 5;
       /// <summary>[String] 英文介绍</summary>
       public const int eninformation = 6;
       /// <summary>[String] 法语名字</summary>
       public const int frname = 7;
       /// <summary>[String] 法语介绍</summary>
       public const int frinformation = 8;
       /// <summary>[String] 西班牙语名字</summary>
       public const int esname = 9;
       /// <summary>[String] 西班牙语介绍</summary>
       public const int esinformation = 10;
       /// <summary>[String] 葡萄牙语名字</summary>
       public const int ptname = 11;
       /// <summary>[String] 葡萄牙语介绍</summary>
       public const int ptinformation = 12;
       /// <summary>[String] 德语名字</summary>
       public const int dename = 13;
       /// <summary>[String] 德语介绍</summary>
       public const int deinformation = 14;
       /// <summary>[String] 韩语名字</summary>
       public const int koname = 15;
       /// <summary>[String] 韩语介绍</summary>
       public const int koinformation = 16;
       /// <summary>[String] 日语名字</summary>
       public const int janame = 17;
       /// <summary>[String] 日语介绍</summary>
       public const int jainformation = 18;
       /// <summary>[String] 俄语名字</summary>
       public const int runame = 19;
       /// <summary>[String] 俄语介绍</summary>
       public const int ruinformation = 20;
       /// <summary>[String] 意大利语名字</summary>
       public const int itname = 21;
       /// <summary>[String] 意大利语介绍</summary>
       public const int itinformation = 22;
       /// <summary>[String] 波兰语名字</summary>
       public const int plname = 23;
       /// <summary>[String] 波兰语介绍</summary>
       public const int plinformation = 24;
   }
   public sealed class z_signact
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体左侧标题</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体左侧标题</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文左侧标题</summary>
       public const int enname = 3;
       /// <summary>[String] 法语左侧标题</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语左侧标题</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语左侧标题</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语左侧标题</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语左侧标题</summary>
       public const int koname = 8;
       /// <summary>[String] 日语左侧标题</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语左侧标题</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语左侧标题</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语左侧标题</summary>
       public const int plname = 12;
   }
   public sealed class z_sprite
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体精灵名字</summary>
       public const int cnname = 1;
       /// <summary>[String] 繁体精灵名字</summary>
       public const int hkname = 2;
       /// <summary>[String] 英文精灵名字</summary>
       public const int enname = 3;
       /// <summary>[String] 法语精灵名字</summary>
       public const int frname = 4;
       /// <summary>[String] 西班牙语精灵名字</summary>
       public const int esname = 5;
       /// <summary>[String] 葡萄牙语精灵名字</summary>
       public const int ptname = 6;
       /// <summary>[String] 德语精灵名字</summary>
       public const int dename = 7;
       /// <summary>[String] 韩语精灵名字</summary>
       public const int koname = 8;
       /// <summary>[String] 日语精灵名字</summary>
       public const int janame = 9;
       /// <summary>[String] 俄语精灵名字</summary>
       public const int runame = 10;
       /// <summary>[String] 意大利语精灵名字</summary>
       public const int itname = 11;
       /// <summary>[String] 波兰语精灵名字</summary>
       public const int plname = 12;
   }
   public sealed class z_story
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体故事内容</summary>
       public const int cncontent = 1;
       /// <summary>[String] 繁体故事内容</summary>
       public const int hkcontent = 2;
       /// <summary>[String] 英文故事内容</summary>
       public const int encontent = 3;
       /// <summary>[String] 法语故事内容</summary>
       public const int frcontent = 4;
       /// <summary>[String] 西班牙语故事内容</summary>
       public const int escontent = 5;
       /// <summary>[String] 葡萄牙语故事内容</summary>
       public const int ptcontent = 6;
       /// <summary>[String] 德语故事内容</summary>
       public const int decontent = 7;
       /// <summary>[String] 韩语故事内容</summary>
       public const int kocontent = 8;
       /// <summary>[String] 日语故事内容</summary>
       public const int jacontent = 9;
       /// <summary>[String] 俄语故事内容</summary>
       public const int rucontent = 10;
       /// <summary>[String] 意大利语故事内容</summary>
       public const int itcontent = 11;
       /// <summary>[String] 波兰语故事内容</summary>
       public const int plcontent = 12;
   }
   public sealed class z_talk
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[Int] 对话的人</summary>
       public const int person = 1;
       /// <summary>[String] 简体中文对话</summary>
       public const int cntalk = 2;
       /// <summary>[String] 简体中文选项</summary>
       public const int cncheck = 3;
       /// <summary>[String] 繁體中文对话</summary>
       public const int hktalk = 4;
       /// <summary>[String] 繁体中文选项</summary>
       public const int hkcheck = 5;
       /// <summary>[String] 英文对话</summary>
       public const int entalk = 6;
       /// <summary>[String] 英文选项</summary>
       public const int encheck = 7;
       /// <summary>[String] 法語对话</summary>
       public const int frtalk = 8;
       /// <summary>[String] 法語选项</summary>
       public const int frcheck = 9;
       /// <summary>[String] 西班牙語对话</summary>
       public const int estalk = 10;
       /// <summary>[String] 西班牙語选项</summary>
       public const int escheck = 11;
       /// <summary>[String] 葡萄牙語对话</summary>
       public const int pttalk = 12;
       /// <summary>[String] 葡萄牙語选项</summary>
       public const int ptcheck = 13;
       /// <summary>[String] 德語对话</summary>
       public const int detalk = 14;
       /// <summary>[String] 德語选项</summary>
       public const int decheck = 15;
       /// <summary>[String] 韓語对话</summary>
       public const int kotalk = 16;
       /// <summary>[String] 韓語选项</summary>
       public const int kocheck = 17;
       /// <summary>[String] 日語对话</summary>
       public const int jatalk = 18;
       /// <summary>[String] 日語选项</summary>
       public const int jacheck = 19;
       /// <summary>[String] 俄語对话</summary>
       public const int rutalk = 20;
       /// <summary>[String] 俄語选项</summary>
       public const int rucheck = 21;
       /// <summary>[String] 義大利語对话</summary>
       public const int ittalk = 22;
       /// <summary>[String] 義大利語选项</summary>
       public const int itcheck = 23;
       /// <summary>[String] 波蘭語对话</summary>
       public const int pltalk = 24;
       /// <summary>[String] 波蘭語选项</summary>
       public const int plcheck = 25;
   }
   public sealed class z_task
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体中文任务标题</summary>
       public const int cntitle = 1;
       /// <summary>[String] 简体中文任务描述</summary>
       public const int cndesc = 2;
       /// <summary>[String] 繁體中文任务标题</summary>
       public const int hktitle = 3;
       /// <summary>[String] 繁体中文任务描述</summary>
       public const int hkdesc = 4;
       /// <summary>[String] 英文任务标题</summary>
       public const int entitle = 5;
       /// <summary>[String] 英文任务描述</summary>
       public const int endesc = 6;
       /// <summary>[String] 法語任务标题</summary>
       public const int frtitle = 7;
       /// <summary>[String] 法語任务描述</summary>
       public const int frdesc = 8;
       /// <summary>[String] 西班牙語任务标题</summary>
       public const int estitle = 9;
       /// <summary>[String] 西班牙語任务描述</summary>
       public const int esdesc = 10;
       /// <summary>[String] 葡萄牙語任务标题</summary>
       public const int pttitle = 11;
       /// <summary>[String] 葡萄牙語任务描述</summary>
       public const int ptdesc = 12;
       /// <summary>[String] 德語任务标题</summary>
       public const int detitle = 13;
       /// <summary>[String] 德語任务描述</summary>
       public const int dedesc = 14;
       /// <summary>[String] 韓語任务标题</summary>
       public const int kotitle = 15;
       /// <summary>[String] 韓語任务描述</summary>
       public const int kodesc = 16;
       /// <summary>[String] 日語任务标题</summary>
       public const int jatitle = 17;
       /// <summary>[String] 日語任务描述</summary>
       public const int jadesc = 18;
       /// <summary>[String] 俄語任务标题</summary>
       public const int rutitle = 19;
       /// <summary>[String] 俄語任务描述</summary>
       public const int rudesc = 20;
       /// <summary>[String] 義大利語任务标题</summary>
       public const int ittitle = 21;
       /// <summary>[String] 義大利語任务描述</summary>
       public const int itdesc = 22;
       /// <summary>[String] 波蘭語任务标题</summary>
       public const int pltitle = 23;
       /// <summary>[String] 波蘭語任务描述</summary>
       public const int pldesc = 24;
   }
   public sealed class z_visitTalk
   {
       /// <summary>[Int] ID</summary>
       public const int id = 0;
       /// <summary>[String] 简体中文聊天轮盘</summary>
       public const int cntalk = 1;
       /// <summary>[String] 繁體中文聊天轮盘</summary>
       public const int hktalk = 2;
       /// <summary>[String] 英文聊天轮盘</summary>
       public const int entalk = 3;
       /// <summary>[String] 法語聊天轮盘</summary>
       public const int frtalk = 4;
       /// <summary>[String] 西班牙語聊天轮盘</summary>
       public const int estalk = 5;
       /// <summary>[String] 葡萄牙語聊天轮盘</summary>
       public const int pttalk = 6;
       /// <summary>[String] 德語聊天轮盘</summary>
       public const int detalk = 7;
       /// <summary>[String] 韓語聊天轮盘</summary>
       public const int kotalk = 8;
       /// <summary>[String] 日語聊天轮盘</summary>
       public const int jatalk = 9;
       /// <summary>[String] 俄語聊天轮盘</summary>
       public const int rutalk = 10;
       /// <summary>[String] 義大利語聊天轮盘</summary>
       public const int ittalk = 11;
       /// <summary>[String] 波蘭語聊天轮盘</summary>
       public const int pltalk = 12;
   }
}
public enum ExcelName
{
   ability = 0,
   ach = 1,
   actShopDay = 2,
   actShopRose = 3,
   air = 4,
   airScene = 5,
   animal = 6,
   animalEffect = 7,
   apple = 8,
   battle = 9,
   birthPos = 10,
   boxOpen = 11,
   build = 12,
   buyGold = 13,
   buyIvary = 14,
   buyLure = 15,
   buyMoney = 16,
   buyMoneyBargain = 17,
   buyMoneyEquip = 18,
   camera = 19,
   cameraMoveByPic = 20,
   client3DConfig = 21,
   configE = 22,
   cook = 23,
   curve = 24,
   effect = 25,
   egg = 26,
   eggBox = 27,
   equip = 28,
   equipKillItem = 29,
   equipSkilled = 30,
   equipType = 31,
   expPasture = 32,
   farmlv = 33,
   festival = 34,
   fishing = 35,
   fishingPer = 36,
   fishProb = 37,
   fishType = 38,
   game = 39,
   gameList = 40,
   gen = 41,
   groundType = 42,
   hole = 43,
   item = 44,
   itemSp = 45,
   itemType = 46,
   lvsk = 47,
   machineFood = 48,
   machineFood2 = 49,
   manorRule = 50,
   milk = 51,
   milkPer = 52,
   monthAct = 53,
   monthHead = 54,
   mshopCloth = 55,
   mshopCloth2 = 56,
   name = 57,
   nameGirl = 58,
   npc = 59,
   npcGift = 60,
   objPath = 61,
   OreHoleSize = 62,
   playerPlane = 63,
   plotPath = 64,
   que = 65,
   queItem = 66,
   scale3DModel = 67,
   scene = 68,
   sceneAreaPos = 69,
   sceneObj = 70,
   sceneStaticObj = 71,
   shop = 72,
   shopAnimal = 73,
   shopCup = 74,
   shopDiy = 75,
   shopEq = 76,
   shopFeed = 77,
   shopHouse = 78,
   shopHouseskin = 79,
   shopMachine = 80,
   shopRest = 81,
   sign = 82,
   sign7 = 83,
   signact = 84,
   sprite = 85,
   spriteGift = 86,
   spriteLv = 87,
   spriteScene = 88,
   stone = 89,
   story = 90,
   sugar = 91,
   talk = 92,
   taskLv = 93,
   taskReward = 94,
   temperature = 95,
   testCropDebuger = 96,
   uiConfig = 97,
   valueConst = 98,
   vip = 99,
   Vipexp = 100,
   z_ach = 101,
   Z_achOri = 102,
   z_actShopRose = 103,
   z_animal = 104,
   z_buyLure = 105,
   z_buyMoneyBargain = 106,
   z_buyMoneyEquip = 107,
   z_eggBox = 108,
   z_festival = 109,
   z_game = 110,
   z_item = 111,
   z_itemSp = 112,
   z_itemType = 113,
   z_lang = 114,
   z_mail = 115,
   z_manorRule = 116,
   z_monthAct = 117,
   z_npc = 118,
   z_OreHoleSize = 119,
   z_scene = 120,
   z_sceneStaticObj = 121,
   z_shopMachine = 122,
   z_signact = 123,
   z_sprite = 124,
   z_story = 125,
   z_talk = 126,
   z_task = 127,
   z_visitTalk = 128,
}