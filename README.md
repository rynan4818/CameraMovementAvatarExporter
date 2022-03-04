# CameraMovementAvatarExporter
[ChroMapper-CameraMovement](https://github.com/rynan4818/ChroMapper-CameraMovement)用のカスタムアバターを作るためのUnityパッケージです。


# 使用方法
1. 適当なUnityのバージョンで新規プロジェクトを作成するか、既存のアバター作成に使用したプロジェクトを別名コピー(シェーダー変更するので)して開きます。
2. [Releases](https://github.com/rynan4818/CameraMovementAvatarExporter/releases)から`CameraMovementAvatarExporter.unitypackage`をダウンロードして、Assets -> Import Package -> Custom Package... でインポートします。

![image](https://user-images.githubusercontent.com/14249877/155548680-e501550c-cf76-4c90-88c6-c4a061947717.png)

## 既存のアバター作成に使用したプロジェクトの場合
3. Hierarchy上にモデルがない場合は、ProjectからprefabをHierarchyにドラッグします。
4. 次の手順は8.へ飛びます。

## VRMファイルをインポートする場合
3. [UniVRM](https://github.com/vrm-c/UniVRM/releases)から`UniVRM-*.**.*_****.unitypackage`をダウンロードして、同じくImport Packageします。
4. Unityプロジェクトのカラースペースの設定がガンマ(Gamma)だと下記画面が出るため、Use recommended Linearを押して、Closeします。

    この画面を飛ばしても、Edit -> Project Settings -> Player -> Other Settings の Rendering項目のColor Space で設定可能です。
    GammaとLinearの違いは[ググる](https://www.google.co.jp/search?q=Unity+Gamma+Linear)と色々出てきます。現在は基本的にLinearで良いです。

![image](https://user-images.githubusercontent.com/14249877/155549796-b9219a2d-02ae-4262-ae96-778975ec5d62.png)

5. VRM0 -> Import from VRM 0.x でVRMファイルを読み込みます。

![image](https://user-images.githubusercontent.com/14249877/155550098-0255b60f-5797-4063-a2ff-278345a34107.png)

6. prefabの保存先が聞かれるので、そのままAssetsフォルダに保存します。

![image](https://user-images.githubusercontent.com/14249877/155550384-0c7911be-8856-4b91-af1f-1fce1e0ff654.png)

7. ProjectのAssetsフォルダに読み込まれたVRMファイルのprefabがあるので、Hierarchyにドラッグします。次の手順は8.へ飛びます。

![image](https://user-images.githubusercontent.com/14249877/155550825-f35a705c-5f89-4a98-9a43-750501d948dd.png)

## unityパッケージ形式で配布されているモデルの場合(VRChat用等)
3. 配布されているモデルの手順に従って、必要ならシェーダーのインポート後にモデルをインポートします。
4. Hierarchy上にモデルがない場合は、ProjectからprefabをHierarchyにドラッグします。

## MaterialのシェーダーをBeatSaber/*に変更します
8. Hierarchyのモデルのツリーを開いて、InspectorにMaterialが割り当てられている物を探します。

![image](https://user-images.githubusercontent.com/14249877/155551861-9fb9dc22-55b5-4c16-8a1e-9c15dfadb25b.png)

9. Materialを開いて、Textureに割り当てられている画像ファイル(クリックするとProjectで自動で該当画像に飛びます)を調べて覚えておきます。

![image](https://user-images.githubusercontent.com/14249877/155552498-62e5606f-c119-4531-9e8c-57e0983cc989.png)

10. Shader を BeatSaber にある Unlit Glow に変更します。

![image](https://user-images.githubusercontent.com/14249877/155552991-08db7d1e-1888-46e3-ba69-b75fa29166b8.png)

11. 変更するとTextureが外れるので、先程調べた画像ファイルを選択し直します。(Selectボタンもしくは、Projectから該当画像をドラッグ＆ペースト)

![image](https://user-images.githubusercontent.com/14249877/155553449-80e04ea7-9e29-4b6d-bdfa-e06fe29b77c1.png)

12. アバターの全てのMaterialのShaderを同様に変更してきます。（これが一番大変）
13. 全てのShaderが変更終わったら、CameraMovement -> MissingAllDelete -> Scene を実行して、Missingなコンポーネントを削除します。

![image](https://user-images.githubusercontent.com/14249877/155554450-cf976c78-bef8-41be-9203-abd3f18396ce.png)

14. Hierarchy上のアバターの一番ルートを選択し、そのまま CameraMovement -> Avatar Exporter を選択します。

![image](https://user-images.githubusercontent.com/14249877/155554887-ea305cfe-3f34-491b-9004-5d0ca2df060f.png)

15. アバターの選択したルートの名前が表示されているのを確認して、Export (アバター名)を押します。

![image](https://user-images.githubusercontent.com/14249877/155555361-73e9ea74-44db-4e8c-b753-3c7e566774a6.png)

16. CameraMovementで読み込めるカスタムアバターファイルが保存できるので、ChroMapper.exeがあるフォルダに保存します。
17. CameraMovementのMoreSettingにあるAvatar FIleに保存した.avatarファイル名を設定して完了です。

![image](https://user-images.githubusercontent.com/14249877/155556191-45ae88d6-cfc5-405f-a525-309cb5287832.png)

![image](https://user-images.githubusercontent.com/14249877/155556336-0dae7a43-ebdc-403a-a26b-95e704d2bb17.png)


# 本リポジトリについて

CameraMovementAvatarExporterは[BeatSaberCustomAvatars](https://github.com/nicoco007/BeatSaberCustomAvatars)の[Unity/BeatSaberCustomAvatars](https://github.com/nicoco007/BeatSaberCustomAvatars/tree/main/Unity/BeatSaberCustomAvatars)からシェーダーと[AvatarExporterWindow.cs](https://github.com/nicoco007/BeatSaberCustomAvatars/blob/main/Source/CustomAvatar-Editor/Editor/AvatarExporterWindow.cs)をコピーし、修正して使用しています。

BeatSaberCustomAvatarsのライセンス[GNU Lesser General Public License v3.0](https://github.com/nicoco007/BeatSaberCustomAvatars/blob/main/LICENSE.txt)に従い、[本ライセンス](https://github.com/rynan4818/CameraMovementAvatarExporter/blob/main/LICENSE.txt)も同様にLGPL3.0とします。
