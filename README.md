# MetaUniversity
- 금오공과대학교를 메타버스 상에서 구현하여 방문자가 재밌게 금오공대를 경험할 수 있는 프로젝트.
- ver.1.0.1
- Web GL 및 Windows 환경에서 실행

<br>


# 시연 영상
https://www.youtube.com/watch?v=v5l_IptxKSk
<br>


# 게임 화면
![image](https://user-images.githubusercontent.com/98202797/206194026-ad01b30c-0956-44bb-907e-58f6dce213bd.png)

<br>

# 프로젝트 활용 방안
- 금오공대의 사진(거리뷰, 위성뷰)으로는 보기 힘든 곳(내부)까지 직접 들어가보는 체험이 가능하다.
- 신입생 및 학부모와 같이 금오공대를 처음 접하거나, 잘 모르는 사람들에게 체험 및 학교에 대한 정보를 제공한다.
- 웹GL을 통해, 학교 홈페이지의 VR체험 메뉴와 연동하여 웹에서의 접근성 높은 활용이 가능하도록 한다.


<br>


# 사용 에셋

### 색상 및 질감 (Mesh & Material)
- !Materials Collection
- GlassPack


### 건물 및 주변환경
- Brick Project Studio
- BrokenVector
- CherryBlossom
- Darth_Artisan
- Fence 4 Pack FREE
- gate
- Polytope Studio
- Fantasy Skybox FREE
- Fence 4 Pack FREE

### NPC 및 캐릭터 / 상호작용
- AnimeGirls
- Kevin Iglesias
- NPC Asset
- TextMesh Pro

### 배경 및 하늘
- 8K Skybox Pack Free



# 사용 오픈소스
- API : OpenWeather, WebGL
- Modeling : Unity Asset Store, Blender 3D Object File
- Script : Git, GitHub



# 사용설명서
- w,a,s,d로 기본적인 이동을 하고, shift키를 누르면 오른쪽 아래에 보이는 스테미너가 없어지기 전까지 달릴 수 있다. space를 누르면 점프하며 f키를 눌러 npc와 대화하거나 포탈을 사용할 수 있다.
- m키를 누르면 맵을 볼 수 있고, 이 상태에서 w,a,s,d를 누르면 맵이 이동하며 마우스 휠을 동작하면 확대 또는 축소할 수 있다.
- 조작법은 프로그램 내에서 f1키를 누르면 볼 수 있고, esc를 누르면 메뉴가 나타난다.
- 현재 단계에서의 기본적인 시나리오는 게임 시작 후 캠퍼스를 올라가면서 퀘스트를 받아 진행한다. 



# 역할 분담

#### 정상경 [sg-Jung]
	- Player/NPC 움직임, NPC 상호작용/대화, NPC별 퀘스트 및 이를위한 Manager구현
	- 씬 변경 및 Teleport 구현
	- Player 기본 ui, 퀘스트 성공시 ui 구현
	- 퀘스트 사운드 구현
	- OpenWearther API에서 불러온 날씨에 따른 인 게임 날씨 설정 기능 구현

#### 김준용 [Vinca0121]
	- 야외 공연장 구역, 버스정류장, 라운지, 학생회관 구역, 테크노관 건물, 기숙사 건물, 
	건물 외부/내부 세부 구조물 구현
	- 팀 로고, 게임 로고 제작
	- 사용자 메뉴얼 관리
	- AWS S3 WebGL 구축 및 관리
	- Git 관리, Readme 작성 

#### 한선재 [HSunJ]
	- 디지털관 내부, 벤처창업관 건물, 도서관 건물, 정문 왼쪽 조경, 정문 왼쪽 주차장 조경, 본관 주차장 구현
	- 보고서 작성
  
#### 김근범 [agfalcon]
	- 차량 애니메이션
	- 성능 테스트 및 렌더링 최적화
	- 게임 시작, 설정, 조작법, 미니맵 ui 및 시스템 구현
	- 정문 왼쪽 주차장, 디지털관 앞 주차장 구현
  
  
#### 김동하 [DongHa2021]
	- 발표 자료 제작 및 발표
	- 디지털관 건물, 본관 건물, 벤처창업관 조경, 정문 구조물, 구현
	- 전체 맵 디테일 조정
	- openweather Api 연동


# Insight
![image](https://user-images.githubusercontent.com/98202797/206202852-6ba2cd28-be09-47f7-9065-77aa81931ad2.png)

![image](https://user-images.githubusercontent.com/98202797/206202928-5bf7794e-bf9b-4359-afa1-19e1e6975be8.png)

