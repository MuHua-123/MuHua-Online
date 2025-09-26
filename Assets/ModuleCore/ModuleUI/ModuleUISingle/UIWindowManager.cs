using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI窗口管理器
/// </summary>
public class UIWindowManager : ModuleUISingle<UIWindowManager> {

	public VisualTreeAsset ServerTemplate;

	public UIOnlineDiscoveryWindow onlineDiscoveryWindow;

	public override VisualElement Element => root.Q<VisualElement>("Window");

	public VisualElement OnlineDiscoveryWindow => Q<VisualElement>("OnlineDiscoveryWindow");

	protected override void Awake() {
		NoReplace(false);
		onlineDiscoveryWindow = new UIOnlineDiscoveryWindow(OnlineDiscoveryWindow, root, ServerTemplate);
	}
}
