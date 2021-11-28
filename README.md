# UnityEscapeGame

### 게임 설명
스테이지 모드와 스토리 모드로 구성된 1p 맵 탈출 게임입니다.<br>
맵 곳곳에는 탈출에 도움이 되는 아이템과 도구, 버튼 등이 있습니다.<br>
플레이어는 마우스와 키보드를 이용해서 그것들을 활성화 할 수 있습니다. <br>


### 공통 기능
이동기능 : wasd 키로 상하좌우 이동 / space 키로 점프 기능<br>
공격기능 : 무기를 얻은 뒤, 숫자키로 무기 장착 / R키로 공격<br>
게임 스테이지 이동<br>
무기와 아이템 수집<br>
게임 종료<br>
타이머<br>
* * * 


### 스테이지 모드
총 9개의 stage로 구성되어 있으며, 플레이어가 맵에 숨어있는 EXIT 버튼 위에 올라가면 탈출이 가능한 맵 탈출 게임입니다.

#### 1) 게임 종료
-맵에 있는 여러 기능을 사용해서 EXIT버튼을 찾아내야합니다.<br>
-버튼이 활성화 됐을 때 그 위에 올라가면 해당 스테이지를 클리어 한 것이 됩니다.<br>
-9개의 스테이지를 모두 다 깨면 게임이 종료됩니다.<br>

#### 2) 해당 모드에서의 부가적인 기능
-마우스 클릭 : 특정 박스의 경우 마우스로 클릭하여 이벤트 실행<br>
-부스터 버튼 : 버튼에 닿은 물체를 기존 방향으로 발사<br>
-박스 이동 : 박스를 밀어서 원하는 곳으로 이동 가능<br>
-텔레포트 : 서로 연결된 위치로 순간 이동<br>

* * *


### 스토리 모드
한 꼬마 아이가 산책을 하다 낯선 마을까지 찾아오며 시작하는 게임입니다.<br>
스토리 모드에서는 동, 식물 등 마을 곳곳의 NPC와 대화를 나누며 탈출의 실마리를 찾아야 합니다.<br>

#### 1) 게임 종료
-스테이지 모드와 동일하게 EXIT 버튼을 찾아 내야합니다.<br>
-버튼이 활성화 됐을 때 그 위에 올라가면 게임이 종료됩니다.<br>

#### 2) 해당 모드에서의 부가적인 기능
-X키를 눌러 NPC와 대화
-Alt 키를 눌러 NPC가 아닌 물체를 탐색
