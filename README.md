# SW_MonsterDataTool

ver1.05


基本ルールブックに記載されている物をそのまま書けばデータ化できるのを目指しました

・動作環境
Windows10でのみ確認済み

・インストール
好きな場所に解凍して、置いてお使いください

・アンインストール
いらなくなったら削除してください

・起動方法
SW_MonsterTool.exeをダブルクリックで起動できます

・著作権について
SW_MonsterTool.exeの著作権は製作者（飛竜）に帰属します
データ等の著作権は制作元のグループＳＮＥ様、著作者の北沢慶様に帰属します
再配布は個人レベルでのみ許可
再配布の場合は以下の事項を守ってください
・無料配布
・このファイルと実行ファイルを一緒に配布

・作成ファイルについて
作成したモンスターデータは自分で編集したデータベースファイルを配布することを許可します
ルールブックのデータは著作権に気を付けてください
自作のデータは配布制限はいたしません

・保証、免責
本ソフトウェアの使用・データベースファイルの配布等により、いかなるトラブルが発生しても一切責任を負いませんのでご了承下さい


---以下説明

<----スプレッドシート機能が書いてありますが安定実装できるまで封印しました---->


・設定項目

初回起動時は設定が何も入っていません
メニューのファイル、設定から同梱してある最低限のConfigCSVを設定してください

魔物CSVは魔物一覧で見たいのできるCSVをセットしてください

設定で読み込みCSVに出力先CSVを設定していると書き込めない場合があります
その場合は読み込み先から削除してから出力してください

・追加

知名度などは-1が例外処理になっています
ルルブを確認した限り-1などがなかったためそうなっているのでもし－の値を設定したい場合は
製作者に連絡をください

プルダウンの中に記載されてない物があるという場合は
ConfigCSVに追加してください。縦で記載してあるのでメモ帳では不便かもしれません
のちにツール内にもConfig変更を追加予定ではあります

また追加しても増えていない場合は直接記入しても出力可能です


・「基礎ステータス」
コア部位がある場合はコア部位を記入してください
ステータスはダブルクリックで編集できます
命中力などはnに固定値を代入する形の記載をお願いします
一部特殊なモンスターも確認したかぎりできるだけ対応しています

・「特殊能力」
左上から３つは宣言の種類を選択できます
確認した限りは公式では最大３つまでだったのでそのようになっています
間違いがあれば修正します
右上は宣言の名称記入欄になってます
一番大きい空欄は概要説明記入欄です
行為判定を使用する能力はは固定値(固定値+7)などをこの欄に書くことを想定しています

・「戦利品」
一番左から取得できるダイス数、名称、個数、売却額、マテリアルカードとなっています
個数などは1dでも記載は大丈夫です
基本直接記入の形になります


モンスターのデータ編集、削除については現在CSVを直接編集する仕様になっています
こちらもそのうち追加を予定しています

モンスターデータの表示がたまにずれたり乱れたりするかもしれません
その場合は他モンスターに切り替えをしたりすればなおることがあります
テキスト表示枠が小さくなっているだけなのでスクロールなどで一応見れはします

大量のデータを導入した場合の動作確認があまりとれていないので
不具合が生じたら連絡をお願いします

不具合、UI改善などがありましたら製作者まで


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
  専用のフォルダ作成し、その中に吐き出します。
　チャットパレットも基本ステータスに合わせて作成します
　特殊能力など所はUdonarium上で各々追加してください
・スプレッドシート機能一時封印
・UI微調整

20/06/16
・ソフト一般公開
