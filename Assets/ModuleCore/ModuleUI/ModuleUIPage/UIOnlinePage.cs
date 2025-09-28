using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 联机页面
/// </summary>
public class UIOnlinePage : ModuleUIPage {

	public VisualTreeAsset ServerTemplate;

	private UIOnlineDiscovery onlineDiscovery;

	public override VisualElement Element => root.Q<VisualElement>("OnlinePage");

	public VisualElement OnlineDiscovery => Q<VisualElement>("OnlineDiscovery");

	protected void Awake() {
		onlineDiscovery = new UIOnlineDiscovery(OnlineDiscovery, root, ServerTemplate);

		ModuleUI.OnJumpPage += ModuleUI_OnJumpPage;
	}

	private void ModuleUI_OnJumpPage(Page page) {
		Element.EnableInClassList("document-page-hide", page != Page.Online);
		onlineDiscovery.Settings(page == Page.Online, null);
		if (page != Page.Online) { return; }
	}
}
