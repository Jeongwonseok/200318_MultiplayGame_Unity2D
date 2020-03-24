Multiplay Game (Unity2D)
======================

# 1. 게임 명
Ball Battle

# 2. 주제
유니티 멀티플레이어 네트워크 게임 제작 (Unity 2D)

# 3. 목적
- Unity, Photon, Firebase를 이용해서 멀티플레이어 네트워크 게임을 제작한다.
- 2명의 플레이어가 공 1개를 이용해 대결을 하는 게임을 제작한다.

****
# 4. 개발 환경
* Unity 2019.2.9f1 Personal
* Visual Studio 2019
* Photon PUN 2
* Firebase Unity SDK

****
# 5. 개발 범위
* Game Manager (게임 매니저)
: Photon Pun의 이벤트 감지, 플레이어 및 공 생성, 스코어 계산, 게임 종료 기능을 정의한다.

* Auth Manager (인증 매니저)
: FirebaseApp, FirebaseAuth 클래스를 이용해 로그인 기능을 정의한다.

* Lobby Manager (로비 매니저)
: 로비 시스템과 매치 메이킹 시스템을 관할하는 기능을 정의한다.

* Player Setting (플레이어 준비)
: 플레이어의 동작을 정의한다.

* Ball Setting (공 준비)
: 공의 이동, 입사각, 반사각, 충돌, 속도를 정의한다.

* Key Mapping (키 매핑)
: 입력 버튼에 따른 상태를 정의한다.

****
# 6. 참고
## 6.1. 스크린 샷
* **Local Player (로컬)**
![ref1](https://github.com/Jeongwonseok/Portfolio_JWS/blob/master/image/pong/local.png)

* **Remote Player (리모트)**
![ref1](https://github.com/Jeongwonseok/Portfolio_JWS/blob/master/image/pong/remote.png)

## 6.2. 참고 URL
* 레트로 [네트워크 게임 만들기] 강좌 : https://www.youtube.com/watch?v=-QsfDgvcheQ&list=PLctzObGsrjfwF7kkoraWb235U8Z602gx1&index=1
