public enum EventDefine
{
    // 状态切换
    BackToMain,
    StartGame,
    GameOver,

    // 显示隐藏界面
    ShowSelectUI,   // 打开初始选择界面
    HideSelectUI,   // 关闭
    ShowShopUI,
    HideShopUI,
    ShowNoticeInfoUI,   // 提示信息

    // 刷新界面
    RefreshWeapon,
    RefreshItem,
    RefreshEnemyCount,
    RefreshPlayerAttribute,
    

    // 怪物死亡
    OneEnemyDeath

}