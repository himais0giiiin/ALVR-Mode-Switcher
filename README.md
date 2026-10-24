# ALVR Mode Switcher

Windows上でTailscaleのサブネット経路受け入れを切り替えるソフトウェアです。
Tailscaleが入った環境でALVRを使用すると、接続が不安定になることがあります。ALVR Mode Switcherを使用し、Tailscaleのサブネット経路受け入れを切り替えることで、ALVRの接続を安定させることができます。

- ALVRモード: `tailscale set --accept-routes=false`
- Tailscaleモード: `tailscale set --accept-routes=true`
- 初回起動時に言語、Tailscaleの場所、ライト／ダークテーマを設定
- 通常画面の「設定」から初期設定を変更可能

設定は次のファイルへ保存されます。

```text
%LocalAppData%\ALVR Mode Switch\settings.json
```