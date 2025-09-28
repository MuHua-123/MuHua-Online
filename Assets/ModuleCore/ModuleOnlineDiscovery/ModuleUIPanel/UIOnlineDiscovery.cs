using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI在线发现
/// </summary>
public class UIOnlineDiscovery : ModuleUIPanel {

	public Action callback;
	public UIScrollViewListV<UIItem, OnlineDiscoveryResponse> items;
	public List<OnlineDiscoveryResponse> discoveredServers = new List<OnlineDiscoveryResponse>();

	public VisualElement ScrollView => Q<VisualElement>("ScrollView");

	public UIOnlineDiscovery(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset) : base(element) {
		items = new UIScrollViewListV<UIItem, OnlineDiscoveryResponse>(ScrollView, canvas, templateAsset,
		(data, element) => new UIItem(data, element, this));
		ModuleUI.AddControl(items);

		OnlineDiscovery<OnlineDiscoveryBroadcast, OnlineDiscoveryResponse>.OnServerFound += OnlineDiscovery_OnServerFound;
	}
	public void OnlineDiscovery_OnServerFound(IPEndPoint sender, OnlineDiscoveryResponse response) {
		discoveredServers.Add(response);
		items.Create(discoveredServers);
	}

	/// <summary> 设置活动状态 </summary>
	public void Settings(bool active, Action callback) {
		this.callback = callback;
		if (!active) { OnlineDiscovery<OnlineDiscoveryBroadcast, OnlineDiscoveryResponse>.I.StopDiscovery(); return; }
		discoveredServers.Clear();
		items.Create(discoveredServers);
		// 更新版本信息
		// GameVersion = ManagerVersion.I.VersionInfo();
		// 发送广播
		OnlineDiscovery<OnlineDiscoveryBroadcast, OnlineDiscoveryResponse>.I.StartClient();
		OnlineDiscovery<OnlineDiscoveryBroadcast, OnlineDiscoveryResponse>.I.ClientBroadcast(new OnlineDiscoveryBroadcast());
	}

	/// <summary> UI项 </summary>
	public class UIItem : ModuleUIItem<OnlineDiscoveryResponse> {
		public readonly UIOnlineDiscovery parent;

		// private DataVersionGame serverVersion;

		public Label Title => element.Q<Label>("Title");
		public Label Count => element.Q<Label>("Count");
		public VisualElement State => Q<VisualElement>("State");

		public UIItem(OnlineDiscoveryResponse value, VisualElement element, UIOnlineDiscovery parent) : base(value, element) {
			this.parent = parent;
			Title.text = $"{value.serverName}[{value.address}]";

			// serverVersion = JsonTool.FromJson<DataVersionGame>(value.serverVersion);
			// if (GameVersion.Equals(serverVersion)) {
			AllowConnection();
			// }
			// else {
			// 	VersionInconsistency();
			// }
		}
		/// <summary> 允许连接 </summary>
		private void AllowConnection() {
			State.EnableInClassList("ow-template-state-g", true);
			element.RegisterCallback<ClickEvent>(ClickEvent);
		}
		/// <summary> 版本不一致 </summary>
		private void VersionInconsistency() {
			State.EnableInClassList("ow-template-state-y", true);
		}

		private void ClickEvent(ClickEvent evt) {
			parent.callback?.Invoke();
			OnlineManager.I.StartClient(value.address.ToString(), value.port.ToString());
		}
	}
}
