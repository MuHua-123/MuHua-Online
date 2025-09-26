using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 联机查询 - UI窗口
/// </summary>
public class UIOnlineDiscoveryWindow : UIWindow {

	public UIOnlineDiscovery onlineDiscovery;

	public VisualElement OnlineDiscovery => Q<VisualElement>("OnlineDiscovery");

	public UIOnlineDiscoveryWindow(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset) : base(element, canvas) {
		ModuleUI.AddControl(this);
		onlineDiscovery = new UIOnlineDiscovery(OnlineDiscovery, canvas, templateAsset);
	}

	/// <summary> 设置活动状态 </summary>
	public override void Settings(bool active) {
		base.Settings(active);
		onlineDiscovery.Settings(active, () => Settings(false));
	}
}
