// using GameFramework.Fsm;
// using UnityEngine;
// using UnityEngine.UI;

// namespace Act_demo
// {
//   /// <summary>
//   /// 随从
//   /// </summary>
//   public class Minion : Entity
//   {

//     [Header("随从费用")]
//     [SerializeField] private Text m_costText;
//     /// <summary>
//     /// 随从攻击力
//     /// </summary>
//     [SerializeField] private Text m_attackText;
//     /// <summary>
//     /// 随从生命值
//     /// </summary>
//     [SerializeField] private Text m_hpText;
//     /// <summary>
//     /// 随从名称
//     /// </summary>
//     [SerializeField] private Text m_nameText;
//     /// <summary>
//     /// 随从信息
//     /// </summary>
//     [SerializeField] private Text m_desText;
//     private bool m_isDie;
//     [SerializeField] private MinionData m_MinionsData;
//     protected override void OnShow(object userData)
//     {
//       base.OnShow(userData);


//       // //创建状态机
//       // IFsm<Minion> fsm = GameEntry.Fsm.CreateFsm<Minion>(
//       //   this,//状态机持有者
//       //        //以下都为状态...
//       //  new GameInitState()
//       //  );

//       // // 启动有限状态机
//       // fsm.Start<GameInitState>();

//       m_MinionsData = userData as MinionData;

//       Name = m_MinionsData.MinionName;
//       m_costText = transform.Find("Canvas/cost/txt").GetComponent<Text>();
//       m_attackText = transform.Find("Canvas/attack/txt").GetComponent<Text>();
//       m_hpText = transform.Find("Canvas/hp/txt").GetComponent<Text>();
//       m_nameText = transform.Find("Canvas/name/txt").GetComponent<Text>();
//       m_desText = transform.Find("Canvas/des/txt").GetComponent<Text>();
//       m_costText.text = m_MinionsData.Cost.ToString();
//       m_attackText.text = m_MinionsData.MinionAttack.ToString();
//       m_hpText.text = m_MinionsData.MinionCurrentHp.ToString();
//       m_nameText.text = m_MinionsData.MinionName;
//       m_desText.text = m_MinionsData.MinionDes;
//     }

//     // 执行 攻击其他随从
//     public void PerformAttack(Minion target)
//     {
//       target.TakeDamage(this, this.m_MinionsData.MinionAttack);
//       this.TakeDamage(target, target.MinionsData.MinionAttack);
//     }
//     /// <summary>
//     /// 受到伤害
//     /// </summary>
//     /// <param name="attackSource">攻击来源</param>
//     /// <param name="damage">伤害值</param>
//     public void TakeDamage(Minion attackSource, int damage)
//     {
//       m_MinionsData.MinionCurrentHp -= damage;
//     }

//     /// <summary>
//     /// 是否死亡
//     /// </summary>
//     public bool IsDie => m_MinionsData.MinionCurrentHp <= 0;
//     /// <summary>
//     /// 随从数据
//     /// </summary>
//     public MinionData MinionsData => m_MinionsData;
//   }
// }
