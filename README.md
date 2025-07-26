# OnewaveGames_Assignment

## 개요
리그 오브 레전드의 '블리츠 크랭크'가 사용하는 '로켓 그랩'과 유사한 스킬을 구현한 테스트 프로젝트
### 테스트 방법
마우스 우클릭으로 이동을 지원
스킬 키(Q)를 입력해 타겟모드에 진입하고, 타겟모드에서 마우스 좌클릭을 실시해 해당 방향으로 스킬 사용가능
스킬을 사용한 뒤, 재사용에 필요한 쿨다운은 기본값으로 5초로 설정


# 설계 방향성
## 스킬 구조
1. **Effect 구조**
  - 확장의 용이성을 위한 EffectBase를 abstract class로 생성(모든 Effect들이 상속받아 일정하게 동작할 수 있도록 만듦)
  - EffectBase를 ScriptableObject로 상속받게해 Skill에 원하는 형태로 조합할 수 있도록 설계
  - 동일한 Effect더라도 다른 수치적인 차이가 존재할 수 있기 때문에 EffectDataBase를 abstract class로 만들고 ScriptbaleObject로 만들어 같은 기능이지만 다른 수치가 적용될 수 있도록 수정
  - 각 EffectBase와 EffectDataBase를 묶어서 EffectEntry로 Wrapping을 진행해 Inspector에서 한 단위로 처리할 수 있도록 관리 용이성을 향상시킴
2. **Skill 구조**
  - Effect를 활용해 스킬의 기능을 분리했기 때문에 Skill은 별도의 데이터를 저장할 구문과 EffectEntry List로 관리
  - TargetingBase abstract class를 만들어 Skill이 소유하게 만들어 스킬마다 다양한 Target방식으로 확장 가능하도록 설계
  - Skill의 관리 편이성을 위해 ScriptbaleObject를 상속받아 관리 용이성을 향상시킴
  - Skill class가 ScriptableObject로 구현되어 있기 때문에, cooldown과 같은 동적인 수치를 제어하기 위해서 Instance class를 별도로 작성
  - InputSystem과의 연동을 위해 Skill-Action을 묶는 SkillBind를 ScriptableObject로 설계해 관리 용이성 향상
3. **Skill Input 구조**
  - Skill 사용 시퀸스를 Skill 버튼 클릭 - 마우스를 이용한 Target확정 - Skill사용의 순으로 하기위한 입력 시스템 구현
  - SkillInputHandler가 Skill HotKey입력과 Target 선택 입력및 Skill Target 취소 입력 구현을 담당
  - SkiiUseController이 Skill의 현재 Target 선택 진입과 선택 시의 구현을 담당
4. **Input System 사용**
  - 추후 Key변경의 용이성을 위해서 Input System활용(action에 따른 사용자 Key 설정을 저장하고 Load해 적용하는 방식으로 활용)
5. **Scritptable Object 사용 이유**
  - 수치의 조정 등이 테스트 중간에도 가능하기 때문에 직관성을 위해 활용

## 실제 구현
1. **Effect**
  - FaceTargetEffect: Skill의 Target 선택 방향으로 Player를 회전시키는 기능(EffectData 불필요)
  - FireProjectileEffect: 투사체 발사를 위한 기능(Effect Data에 투사체에 충돌했을 때, 발생시킬 Effect, 투사체 Object, 투사체 속도)
  - PullEffect: 효과 대상을 당기는 효과(처리는 당겨지는 대상에게 이관)
2. **Skill**
  - DirectionTargeting: Target 선택을 방향으로 지정
  - SkillData: Skill들이 모두 공통으로 가지고있는 정보를 저장
  - EffectEntry: Skill들의 실질적인 기능 Set을 담당
  - SkillSet: Skill-HotKey bind class 및 Skill Instance 저장
  - SkillInputHandler: SkillSet과 Target 선택 및 HotKey 입력, Target 선택 상태 기능 구현
  - SkiiUseController: Target 선택에 따른 SKill의 사용과 Skill의 Target 선택 상태로 진입 가능 여부 등에 대한 기능 구현

## Dummy와 Player
1. **Player**
  - Player는 Action과 SkillInputHandler를 연결하고 움직임 및 Cooldown소모를 관리
2. **Dummy**
  - 당겨지는 행동의 주체를 Skill과 시전자가 아닌 당하는 대상으로 구현


# ScriptableObject 구조
- Effect의 최소 로직은 EffectBase를 상속받아 구현(생성 가능한 Asset은 EffectBase를 구체화한 class)
- Effect의 최소 로직에 필요한 데이터는 EffectBaseData를 상속받아 구현(생성 가능한 Asset은 EffectBaseData를 구체화한 class)
- EffectBase와 EffectBaseData를 묶어 EffectEntry로 관리(별도의 ScriptableObject는 없고, Serializable한 class, Inspector 관리의 용이성을 위함)
- Skill의 Target방식은 TargetingBase를 상속받아 구현(생성 가능한 Asset은 TargetingBase를 구체화한 class)
- Skill은 SkillData/TargetingBase/EffectEntry를 소유한 형태로 구성(Skill은 ScriptableObject을 상속받아 Asset 생성 및 관리가능)
- SkillSet에서 Skill과 HotKey를 Bind해 관리(캐릭터가 증가하거나 스킬이 늘어났을 때, 관리가 용이하도록 ScriptableObject를 활용)
