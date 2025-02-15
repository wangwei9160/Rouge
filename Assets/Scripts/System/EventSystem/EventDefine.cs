public enum EventDefine
{
    // 状态切换
    BackToMain,
    StartGame,
    GameOver,

    // 显示隐藏界面
    ShowMainScene,  // 初始界面
    HideMainScene,  // 关闭
    ShowDataFileUI, // 存档界面
    HideDataFileUI, // 关闭
    ShowSelectUI,   // 打开初始选择界面
    HideSelectUI,   // 关闭
    ShowShopUI,
    HideShopUI,
    ShowNoticeInfoUI,   // 提示信息

    // 带参数的显示
    ShowPanelByIDInSelectPannel,

    // 刷新界面
    RefreshWeapon,
    RefreshItem,
    RefreshEnemyCount,
    RefreshPlayerAttribute,

    // 刷新界面带参数
    RefreshWeaponByID,
    UpTalentByID,
    RefreshDamageByID,

    // 怪物死亡
    OneEnemyDeath,


    // 获得道具效果
    OnGetFree       // 获得免费刷新次数
}