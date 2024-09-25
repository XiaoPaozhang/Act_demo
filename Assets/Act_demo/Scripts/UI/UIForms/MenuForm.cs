
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class MenuForm : UGuiForm
  {
    private ProcedureMenu m_procedureOwner;
    protected override void OnOpen(object userData)
    {
      m_procedureOwner = userData as ProcedureMenu;
      base.OnOpen(userData);
    }

    public void OnStartBtnClick()
    {
      Close();

      m_procedureOwner.StartGame();
    }

    public void OnQuitBtnClick()
    {


    }
  }
}
