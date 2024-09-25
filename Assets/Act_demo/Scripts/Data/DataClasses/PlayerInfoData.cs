using System.Collections.Generic;
using cfg;
using UnityEngine;
using UnityGameFramework.Runtime;
using QFramework;


namespace Act_demo
{
  public class PlayerInfoData
  {
    //编号
    public BindableProperty<int> Id { get; private set; }
    // 最大血量
    private BindableProperty<int> _maxHp = new BindableProperty<int>();
    public BindableProperty<int> MaxHp
    {
      get => _maxHp;
      set
      {
        //当前血量大于最大血量时，当前血量等于最大血量
        if (CurrentHp.Value > value.Value)
        {
          CurrentHp.Value = value.Value;
        }

        _maxHp.Value = value.Value;
      }
    }

    // 当前血量
    private BindableProperty<int> _currentHp = new BindableProperty<int>();
    public BindableProperty<int> CurrentHp
    {
      get => _currentHp;
      set
      {
        int newValue = Mathf.Clamp(value.Value, 0, MaxHp.Value);
        _currentHp.Value = newValue;
      }
    }
    // 最大法力值
    private BindableProperty<int> _maxMp = new BindableProperty<int>();
    public BindableProperty<int> MaxMp
    {
      get => _maxMp;
      set
      {
        //当前法力值大于最大法力值时，当前法力值等于最大法力值
        if (CurrentMp.Value > value.Value)
        {
          CurrentMp.Value = value.Value;
        }

        _maxMp.Value = value.Value;
      }
    }
    // 当前法力值
    private BindableProperty<int> _currentMp { get; set; } = new BindableProperty<int>();
    public BindableProperty<int> CurrentMp
    {
      get => _currentMp;
      set
      {
        int newValue = Mathf.Clamp(value.Value, 0, MaxMp.Value);
        _currentMp.Value = newValue;
      }
    }
    // 星级
    public BindableProperty<int> Star { get; private set; }
    //拥有的手牌
    public List<CardData> Cards { get; private set; }
    //场地上的随从
    public List<MinionData> Minions { get; private set; }
    public PlayerInfoData(int playerId = 30001)
    {
      // 读表
      Tables tables = GameEntry.LubanComponent.GetTables<Tables>();
      cfg.PlayerInfo playerInfo = tables.TbPlayerInfo.Get(playerId);
      if (playerInfo == null)
      {
        Log.Error("Cfg not found: TbPlayerInfo.Id={0}", playerId);
        return;
      }

      Id = new BindableProperty<int>(playerInfo.Id);
      MaxHp = new BindableProperty<int>(playerInfo.Hp);
      MaxMp = new BindableProperty<int>(playerInfo.Mp);
      CurrentHp = new BindableProperty<int>(playerInfo.Hp);
      CurrentMp = new BindableProperty<int>(playerInfo.Mp);
      Star = new BindableProperty<int>(playerInfo.Star);
      Cards = new List<CardData>();
      Minions = new List<MinionData>();
    }


  }
}

