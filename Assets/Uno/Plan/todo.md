this is a self-use plan file for this project



逻辑编写阶段

6.3
- [x] 植入fishnet插件
- [x] 学习使用
- [x] 成功注册所有ui

6.8
- [x] 牌的排序算法
- [x] 玩家连接事件callback
- [x] 玩家退出事件callback
- [x] room panel完成

6.12
- [ ] 编写游戏逻辑所有接口

6.16
- [ ] 编写singlePlayerGameMode


项目制作标准：


EventManager只使用在本地逻辑上，InstanceFinder则为网络事件的接收器

client/server 代码分开写！！！

single player mode

- [x] init 
  - [x] init card randq DONE
  - [x] called by gamemode, set player count
  - [x] set game mode
- [ ] start game init
  - [x] add playerCode
  - [ ] add playerEntity
    - [x] disable camera
    - [x] disable my control to other card outside my handcard
    - [ ] don't show play card panel when not my round
  - [ ] init player loop
  - [ ] deal cards
  - [ ] show game panel
  - [ ] add aiEntity
  - [ ] decide mecode
  - [ ] 
- [ ] decide how to communicate with local logic
  - [ ] connect call from panels
  - [ ] make drag play card function
- [ ] game loop
  - [ ] next turn
  - [ ] play card
- [ ] game end
  - [ ] show result