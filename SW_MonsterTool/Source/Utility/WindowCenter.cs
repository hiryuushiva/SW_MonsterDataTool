using System;
using System.Windows.Forms;

namespace SW_MonsterTool
{
    //ウィンドウなどをフォームの真ん中に出すためのクラス
    public class WindowCenter
    {
        private IntPtr m_HHook;
        private WindowsAPI.HOOKPROC m_hOOKPROC;
        private IntPtr m_Handle;

        public WindowCenter()
        {

        }

        public void SetHook(IWin32Window owner)
        {
            // フック設定
            m_Handle = owner.Handle;
            IntPtr hInstance = WindowsAPI.GetWindowLong(m_Handle, (int)WindowsAPI.WindowLongParam.GWLP_HINSTANCE);
            IntPtr threadId = WindowsAPI.GetCurrentThreadId();
            m_hOOKPROC = new WindowsAPI.HOOKPROC(CBTProc);
            m_HHook = WindowsAPI.SetWindowsHookEx((int)WindowsAPI.HookType.WH_CBT, m_hOOKPROC, hInstance, threadId);
        }

        private IntPtr CBTProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == (int)WindowsAPI.HCBT.HCBT_ACTIVATE)
            {
                WindowsAPI.RECT rectOwner;
                WindowsAPI.RECT rectMsgBox;
                int x, y;

                // オーナーウィンドウの位置と大きさを取得
                WindowsAPI.GetWindowRect(m_Handle, out rectOwner);
                // MessageBoxの位置と大きさを取得
                WindowsAPI.GetWindowRect(wParam, out rectMsgBox);

                // MessageBoxの表示位置を計算
                x = rectOwner.Left + (rectOwner.Width - rectMsgBox.Width) / 2;
                y = rectOwner.Top + (rectOwner.Height - rectMsgBox.Height) / 2;

                //MessageBoxの位置を設定
                WindowsAPI.SetWindowPos(wParam, 0, x, y, 0, 0,
                  (uint)(WindowsAPI.SetWindowPosFlags.SWP_NOSIZE | WindowsAPI.SetWindowPosFlags.SWP_NOZORDER | WindowsAPI.SetWindowPosFlags.SWP_NOACTIVATE));

                // フック解除
                WindowsAPI.UnhookWindowsHookEx(m_HHook);
            }
            // 次のプロシージャへのポインタ
            return WindowsAPI.CallNextHookEx(m_HHook, nCode, wParam, lParam);
        }

    }

}
