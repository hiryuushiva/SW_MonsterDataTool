# SW_MonsterDataTool

ver1.1

**＊＊21/03/10にアップデート＊＊**
！編集機能の追加！
！import機能の追加！


基本ルールブックに記載されている物をそのまま書けばデータ化できるのを目指しました


**・できること**


ソードワールド2.0および2.5に対応したモンスターのデータ化

データ化したモンスターデータ閲覧、検索、絞り込み

Udonariumへのキャラコマ吐き出し


**・作成目的**

ソードワールドを行う上でシナリオに合ったモンスターを選びたかったり

オフで自分のGMのツールとして使えたら、オンセでの記入の手間を省略できたらなということから作成しました


**・動作環境**

Windows10でのみ確認済み


**・インストール**

好きな場所に解凍して、置いてお使いください


**・アンインストール**

いらなくなったら削除してください


**・起動方法**

SW_MonsterTool.exeをダブルクリックで起動できます


**・著作権について**

SW_MonsterTool.exeの著作権は製作者（飛竜）に帰属します

データ等の著作権は制作元のグループＳＮＥ様、著作者の北沢慶様に帰属します

再配布は個人レベルでのみ許可

再配布の場合は以下の事項を守ってください

・無料配布

・このファイルと実行ファイルを一緒に配布


**・作成ファイルについて**

作成したモンスターデータは自分で編集したデータベースファイルを配布することを許可します

ルールブックのデータは著作権に気を付けてください

自作のデータは配布制限はいたしません


**・保証、免責**

本ソフトウェアの使用・データベースファイルの配布等により、いかなるトラブルが発生しても一切責任を負いませんのでご了承下さい

**・起こるかもしれない不具合**

URLsを直接編集し、最後の行を空白にした場合エラーとなって起動しないようです

対処法は一度ファイルを削除、または空欄にした場所を||||を記入してください

-----
以下説明


**・設定項目**

![設定例](https://user-images.githubusercontent.com/66992843/84762700-1cefee80-b006-11ea-96dc-f83f7c0d5289.png)

初回起動時は設定が何も入っていません

メニューのファイル、設定から同梱してある最低限のConfigCSVを設定してください

記入するファイルはtxt、csvなどに対応しています

記入したいファイルを作成して選択してください

魔物CSVは魔物一覧で見たいCSVをセットしてください

設定で読み込みCSVに出力先CSVを設定していると書き込めない場合があります

その場合は読み込み先から削除してから出力してください

スプレッドシート記載場所がありますが現在動作を封じています


**・追加**

![記入例](https://user-images.githubusercontent.com/66992843/84762755-2da06480-b006-11ea-9a55-64697b29cc4c.png)


知名度などは-1が例外処理になっています

ルルブを確認した限り-1などがなかったためそうなっているのでもし－の値を設定したい場合は

製作者に連絡をください


プルダウンの中に記載されてない物があるという場合は

ConfigCSVに追加してください。縦で記載してあるのでメモ帳では不便かもしれません

のちにツール内にもConfig変更を追加予定ではあります

また追加しても増えていない場合は直接記入しても出力可能です


**・「基礎ステータス」**

コア部位がある場合はコア部位を記入してください

ステータスはダブルクリックで編集できます

命中力などはnに固定値を代入する形の記載をお願いします

一部特殊なモンスターも確認したかぎりできるだけ対応しています

**・「特殊能力」**

左上から３つは宣言の種類を選択できます

確認した限りは公式では最大３つまでだったのでそのようになっています

もし3つ以上が存在すれば修正します

右上は宣言の名称記入欄になってます

一番大きい空欄は概要説明記入欄です

行為判定を使用する能力はは固定値(固定値+7)などをこの欄に書くことを想定しています

**・「戦利品」**

一番左から取得できるダイス数、名称、個数、売却額、マテリアルカードとなっています

個数などは1dでも記載は大丈夫です

基本直接記入の形になります

-----

**・モンスターデータ一覧例**

![一覧例](https://user-images.githubusercontent.com/66992843/84762797-3a24bd00-b006-11ea-9106-79f773017226.png)

モンスターデータを設定していると読み込んでここで確認できます

-----

**・Udonarium出力画面例**

![ユドナリウム出力例](https://user-images.githubusercontent.com/66992843/84762895-5e809980-b006-11ea-9b50-1ace601c3065.png)

2.0なら選択ルールを採用するかどうかが選べます

また剣の欠片を部位ごとに入れることが可能です

-----

**編集**

メイン画面上の編集タブからアクセスできます

Configの編集と既に作成してあるモンスターデータの編集ができます

**・Config編集**

![Config編集例](https://user-images.githubusercontent.com/66992843/110561537-31bf7400-818b-11eb-9c41-5b328ec55ec3.png)

改行してどんどん記入していけば追加されるような仕組みです

Page1に知識、知覚、反応、言語、弱点

Page2に特殊能力、移動、分類、出典があります

簡単に実装したため変にずらして記入したり、変わった文字を入れると不具合が出るかもしれません

その時は報告して頂ければ修正します

**・データ編集**

![データ編集例](https://user-images.githubusercontent.com/66992843/110561845-be6a3200-818b-11eb-908e-08f2ade99b43.png)

小さい画面で編集するデータを選んで大きい画面の方で実際に編集をします

「選択したモンスターを編集」で選んだモンスターを大きい画面にセットします

まとめて保存方式を取っているのであるモンスターの編集中に別のモンスターを開いてもらっても

変更結果は保存するまで残っています

「選択したモンスターを削除」で選んだモンスターを編集リストから削除します

「編集したデータを保存」はこの画面で行った削除、編集を行ったデータを出力します

保存するとUIは初期化されるので気を付けてください

大きい方は追加と大体一緒の動作をします

右下に画像を入れる場所がありますが画像関連の処理は現在未実装です

-----

**・Import**

![Import例](https://user-images.githubusercontent.com/66992843/110561967-fa04fc00-818b-11eb-8b2b-3de52ad971e1.png)

ファイルのImportからアクセスできます

このツールを使った形式のモンスターデータをモンスター追加画面にImportすることができます

既存のモンスターから改変したい、という場合に便利かもしれません

-----

モンスターデータ、Config編集にようやく対応しました

実装したばかりで不具合があるかもしれませんが不具合を見つけたら報告お願いします

モンスターデータの表示がたまにずれたり乱れたりするかもしれません

その場合は他モンスターに切り替えをしたりすればなおることがあります

テキスト表示枠が小さくなっているだけなのでスクロールなどで一応見れます

大量のデータを導入した場合の動作確認があまりとれていないので不具合が出るかもしれません

UIなどの改善目的、不具合などありましたらぜひ連絡をください

Twitter @hiryuu_shiva

--------------------------
更新

20/04/25

・戦利品記入なしでずれることがあるバグ修正？

・UI調整

・出力関係調整

20/04/27

・設定したURLと違うとエラー起きて起動しないバグを修正

20/05/05

・Udonariumのキャラクターコマ対応

専用のフォルダ作成し、その中に吐き出します
  
チャットパレットも基本ステータスに合わせて作成します
 
特殊能力など所はUdonarium上で各々追加してください
 
・スプレッドシート機能一時封印

・UI微調整

20/06/16

・ソフト一般公開

・軽い不具合があったので修正しました

21/03/10

・アップデート

Import、データ編集、Config編集を追加

また製作者が確認している不具合の修正、微調整を行いました
（複数部位追加が機能しないなど）
