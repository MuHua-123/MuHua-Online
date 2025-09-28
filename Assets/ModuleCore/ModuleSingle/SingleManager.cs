using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 全局管理器
/// </summary>
public class SingleManager : ModuleSingle<SingleManager> {

	protected override void Awake() => NoReplace();

	private void Start() {
		UIShortcutMenu shortcutMenu = UIPopupManager.I.shortcutMenu;
		shortcutMenu.Add("页面/无", () => { ModuleUI.Settings(Page.None); });
		shortcutMenu.Add("页面/联机页面", () => { ModuleUI.Settings(Page.Online); });

		shortcutMenu.Add("启动/主机", OnlineManager.I.StartHost);
		shortcutMenu.Add("启动/服务器", OnlineManager.I.StartServer);
	}
}
