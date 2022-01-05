using System;
using System.Windows.Forms;
using System.Threading;
using Memory;
using System.Runtime.InteropServices;

namespace bop
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey); // hotkey
        [DllImport("user32.dll")]
        public static extern void mouse_event(int a, int b, int c, int d, int bruh);

        /* 
         * module = client.dll
         * localplayer = 0xD8C2CC
         * m_fFlags = 0x104
         * dwForceJump = 0x524DEDC
        */
        string localplayerAndmfFlag = "client.dll+0xDB558C,0x104"; // module + localplayer + fFlag
        string ForceJump = "client.dll+0x527A9AC"; // module + forcejump
        string crosshair = "client.dll+0xDB558C,0x11838";
        string team = "client.dll+0xDB558C,0xF4";
        string flashDuration = "client.dll+0xDB558C,0x10470";
        string observe = "client.dll+0xDB558C,0x3388";
        string fov = "client.dll+0xDB558C,0x31F4";
        string spotted = ",0x93D";
        string glowIndex = ",0x10488";
        string glowManager = "client.dll+0x5318E50,";
        int leftDown = 0x02;
        int leftUp = 0x04;

        Mem m = new Mem(); // memory read and write class 
        int flag;
        int shoot;
        int myTeam;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

        }

        void CHEAT()
        {
            while (true)
            {
                myTeam = m.ReadInt(team);
                    if (triggerBox.Checked)
                    {                                                    //entity list                               //idk
                            string CurrentStr = "client.dll+" + "0x" + (0x4DD0AB4 + (m.ReadInt(crosshair) - 1) * 0x10).ToString("X");
                            shoot = m.ReadInt(crosshair);
                            if (shoot > 0)
                            {
                                if (myTeam != m.ReadInt(CurrentStr + ",0xF4") && (m.ReadInt(CurrentStr + ",0xF4") == 2 || m.ReadInt(CurrentStr + ",0xF4") == 3)) {
                                    
                                    mouse_event(leftDown, 0, 0, 0, 0);
                                    Thread.Sleep(20);
                                    mouse_event(leftUp, 0, 0, 0, 0);
                            }
                        }
                    }
                //https://www.youtube.com/channel/UC4et5EGjwrSzE7nA6AlZ37g his code
                if (GetAsyncKeyState(Keys.Space) < 0) // If spacebar is down 
                {
                    flag = m.ReadInt(localplayerAndmfFlag);
                    if (flag == 257 || flag == 263) // if we are grounded, standing or crouching
                    {
                        if (bhopBox.Checked)
                        {
                            m.WriteMemory(ForceJump, "int", "5"); // +jump
                            Thread.Sleep(20);
                            m.WriteMemory(ForceJump, "int", "4"); // -jump
                        }
                    }
                }
                if (flashBox.Checked)
                {
                    if (m.ReadInt(flashDuration) > 0)
                    {
                        m.WriteMemory(flashDuration, "int", "0");
                    }
                }
                if (radarBox.Checked)
                {
                    for (int i = 1; i < 64; i++)
                    {                                                 //entity list
                        string currentEntity= "client.dll+" + "0x" + (0x4DD0AB4 + (i * 0x10)).ToString("X");
                        m.WriteMemory(currentEntity + spotted, "int", "1");
                        
                    }
                }
                if (glowBox.Checked)
                {
                    for (int i = 1; i < 30; i++)
                    {
                                                                        //entity list
                        string currentEntity = "client.dll+" + "0x" + (0x4DD0AB4 + (i * 0x10)).ToString("X");
                        int current = m.ReadInt(currentEntity);

                        if (current != null)
                        {
                            int currentTeam = m.ReadInt(currentEntity + ",0xF4");
                            int currentIndex = m.ReadInt(currentEntity + ",0x10488");
                            string currentGlow = currentEntity + glowIndex;

                            if (true)//(myTeam != currentTeam)
                            {
                                Console.WriteLine(m.ReadInt(currentEntity + ",0xF4"));
                                Console.WriteLine(currentIndex);
                                m.WriteMemory(glowManager + (currentIndex * 0x38 + 0x8), "float", "1");
                                m.WriteMemory(glowManager + (currentIndex * 0x38 + 0xC), "float", "0");
                                m.WriteMemory(glowManager + (currentIndex * 0x38 + 0x10), "float", "0");
                                m.WriteMemory(glowManager + (currentIndex * 0x38 + 0x14), "float", "1");
                                m.WriteMemory(glowManager + (currentIndex * 0x38 + 0x28), "int", "1");
                                m.WriteMemory(glowManager + (currentIndex * 0x38 + 0x29), "int", "0");
                            }
                        }
                    }
                }
                if (checkBox1.Checked)
                {
                    m.WriteMemory(fov, "int", trackBar1.Value.ToString());
                }
                if (thirdBox.Checked)
                {
                    m.WriteMemory(observe, "int", "1");
                }
                else
                {
                    m.WriteMemory(observe, "int", "0");
                }
                if (GetAsyncKeyState(Keys.N) < 0)
                {
                    if (thirdBox.Checked)
                    {
                        this.thirdBox.Checked = false;
                        Thread.Sleep(100);
                    }
                    else
                    {
                        this.thirdBox.Checked = true;
                        Thread.Sleep(100);
                    }
                }
                Thread.Sleep(1);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bhopBox_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("bhop");
        }

        private void triggerBox_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("trigger");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int PID = m.GetProcIdFromName("csgo"); // get proc id from csgo.exe, check if running)
            if (PID > 0)
            {
                m.OpenProcess(PID);
                Thread bh = new Thread(CHEAT) { IsBackground = true }; // Create a separate thread in the background to run our bhop loop
                bh.Start();
                MessageBox.Show("attached to csgo proccess!");
                label2.Text = "ATTACHED";
                label2.Refresh();            
            }
            else
            {
                MessageBox.Show("csgo proccess not found!");
                label2.Text = "FAILED TO ATTACH";
                label2.Refresh();
            }
        } 

        private void flashBox_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("flash");
        }

        private void radarBox_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("radar");
        }

        private void topBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void thirdBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}































// junk code start
//
//
//
//bruh

//Generated by cJunk
public class ItLBWTBWwDlJNDNdhWHRxsNsRbjtGVHYf
{
    void pojzBziJINniJIgwEMesrVPgTkvkIQbwNdYOedfXYHofZJHSCwBIcfnTYCgRDyBslqUfkRSagHUjgjGJddhLOTTiq() { string EqbxOTaGwuipbikdbbVKDmowWoaQpdSXmpfbr = "atJQScLCmCJmyKhQaCzZCmfTHraZPDMaMBrukNSMLRPsnvnhQGMQBE"; string cRoudkpXUBvwhmrxXmSBeb = "YnNqIbBlnjpjaxSmgMknNkXmgMDtJIqwYHcDhuFqKaegCEUqO"; EqbxOTaGwuipbikdbbVKDmowWoaQpdSXmpfbr = "eazfOrnQaKvYIcestKSUPF"; EqbxOTaGwuipbikdbbVKDmowWoaQpdSXmpfbr = cRoudkpXUBvwhmrxXmSBeb; }
}

//Generated by cJunk
public class mHtcnGrkbrMfeirQKQpnGkJSCxAoJtCWyZosmmDhYqiDLcxwAczBfc
{
    void zCxbZqFDbipqgFuyeYNHOPBGGZzDngtAqWZlvLMNbVOzNVwsCCxboXNjArUzqCQuoozJfIDTLzftBCsldHBkjSMSJqlEqNW() { Int32 ZETLZVmMuzeQpsTiyyjVjAfLW = -17289563; Int32 cgApWzdxcFphqTPruHlEBWapInFGrnZYIpwolWXABSMmeBHfTeotuGUFGLhitWGpgQYAWbGXjlDiKn = -86776661; ZETLZVmMuzeQpsTiyyjVjAfLW = -22978446; ZETLZVmMuzeQpsTiyyjVjAfLW = cgApWzdxcFphqTPruHlEBWapInFGrnZYIpwolWXABSMmeBHfTeotuGUFGLhitWGpgQYAWbGXjlDiKn; }
}

//Generated by cJunk
public class KGfamNzfeBUqiapoGeMXGOcHIsXQJJClvS
{
    void AaDTjtpAmWffsgiueZWnhweYRupiWUzivQZOlnuSh() { float FbFbdzriPfdPHkDweBpDHeRXw = -4597981.0f; float jcGTJqfDFbUYwouxAHpVTofVSGmFOabKietWlOFp = -44278259.0f; FbFbdzriPfdPHkDweBpDHeRXw = -38022844.0f; FbFbdzriPfdPHkDweBpDHeRXw = jcGTJqfDFbUYwouxAHpVTofVSGmFOabKietWlOFp; }
}

//Generated by cJunk
public class yLBIiXiMuGGXcBdvxkACDZofAKqSvYMlBzXImXnvYZKITDhgBxZANDlYWVHYgPRWt
{
    void UWceUIaxFeePgMwdKQxyLYxOzlQPOOIHoOyhEBgozCIZHIqJLvVyUXhwYOfisLiqVWbROPYJ() { int kIzIPYHqvHDTtJnRgBJqWxwoUSDkhYuwWMOLXZhzHpnkHOxLiKkMFi = -4365670; int mmMLoxUWIWNHxgvctHUJihKMgFVsBX = -44161064; kIzIPYHqvHDTtJnRgBJqWxwoUSDkhYuwWMOLXZhzHpnkHOxLiKkMFi = -93823628; kIzIPYHqvHDTtJnRgBJqWxwoUSDkhYuwWMOLXZhzHpnkHOxLiKkMFi = mmMLoxUWIWNHxgvctHUJihKMgFVsBX; }
}

//Generated by cJunk
public class ohqoZczMkkelgQXzRQQGQcswzFbzeibcUkifRIGSdhXkexA
{
    void uohoEcvGLpupTBzhBICQvhsiIKxdjFHRBFhLxuXzZdXpXTmRezVcchENwygufSXGhcnOTojHkCrRJCLoXfzjjfpUATOzOdUH() { bool GRHugYxqQOrZpmDunrWzSnWCnTNzZktHikLsvRIziJTVoPeNQUMWGVoSvFGNKxQgUjrFbKyVjXRJUANYozjFYWBMw = true; bool sxpKvpksnAFYYlFVqnjEeyuriYKOKHyheJjNILoh = false; GRHugYxqQOrZpmDunrWzSnWCnTNzZktHikLsvRIziJTVoPeNQUMWGVoSvFGNKxQgUjrFbKyVjXRJUANYozjFYWBMw = false; GRHugYxqQOrZpmDunrWzSnWCnTNzZktHikLsvRIziJTVoPeNQUMWGVoSvFGNKxQgUjrFbKyVjXRJUANYozjFYWBMw = sxpKvpksnAFYYlFVqnjEeyuriYKOKHyheJjNILoh; }
}

//Generated by cJunk
public class gQhAIzPOdkiiSIRtCycWeVifBXtPCsMezBXNRujiIwlaZPPVrpKKiRDKiBykFALlXDjQlEcIOtNikIFmHYXMhCvh
{
    void yMePvycIkzCWgykmNCvpDUZawLdhczEVHBSYZMMQQFxlFj() { string FJnkyeGGedmETjVvflvhOefMNApfYyXoUMGTurAZPBXplVtcvVZMpYUbdaXxp = "UNqinjgplLCTnMHxolppxYzinRsfdMEiMZVOAwSKRycnJOgWffGdQxJwOIvVrBLZGUGOqsxrSeYyvLZYlVAyFaRPQ"; string aifFxkGdBAnlWRNHparHMkCZLzvseqTUCjoTrknlmytUlbSGGTnzgqcqfGuyoOzLhTimduPFnG = "ZAsBwTQnDgoHRgmhPLlZLACiWkVBN"; FJnkyeGGedmETjVvflvhOefMNApfYyXoUMGTurAZPBXplVtcvVZMpYUbdaXxp = "TgCrxRsFpVLILsekRTpNQqPgqFYyJfMxPZjAmQUPucZpZQAHXyFQ"; FJnkyeGGedmETjVvflvhOefMNApfYyXoUMGTurAZPBXplVtcvVZMpYUbdaXxp = aifFxkGdBAnlWRNHparHMkCZLzvseqTUCjoTrknlmytUlbSGGTnzgqcqfGuyoOzLhTimduPFnG; }
}

//Generated by cJunk
public class fnxdRWAssuwKJOtlIeyAzaTnQaGZFRa
{
    void KZvRbcGjPPbWzgOfLXOyszERFUJMrQamaTlkNwRJCvilXDTHCuuCqSiLV() { Int32 eMGStrzopvgudSPFFwxQTtBBCKwVYvIlaPikLqJDjwiHmMYcexlWkuClKFQjudQtnvUO = -8075101; Int32 jfuOnthXKtOpmUrbKmAfMxZFaZIGfpUrrJtzCWjrMfaRqaxamoiBuoBlFooNkbrVZXxcbrYnCIThWIPzciU = -72986393; eMGStrzopvgudSPFFwxQTtBBCKwVYvIlaPikLqJDjwiHmMYcexlWkuClKFQjudQtnvUO = -18976201; eMGStrzopvgudSPFFwxQTtBBCKwVYvIlaPikLqJDjwiHmMYcexlWkuClKFQjudQtnvUO = jfuOnthXKtOpmUrbKmAfMxZFaZIGfpUrrJtzCWjrMfaRqaxamoiBuoBlFooNkbrVZXxcbrYnCIThWIPzciU; }
}

//Generated by cJunk
public class rGTwDIHtOiWGloFsTdVzuPuymVUPxYokCGzOFauTUxKFgChtRQwWHjZbLVKhorvpulEeoqmsiBewFpRjoa
{
    void xhcScThYCkQMfcCUKvyCEadEnGEcVWDasNuNMcIkxQnBbVVCuleWlmVWEYeWPFCghpUSioHim() { float NEonccRlBhkrhpGrXJoxmVsNGUmBCUGdvjYhhjbwMlNXRpzANaKeTlAsMagIqQihaHvyQZKnirlAHUvRUlocJcJKjfUGDSGwDg = -27508284.0f; float wKuBjoQXwbAzksePoEbFXy = -7542235.0f; NEonccRlBhkrhpGrXJoxmVsNGUmBCUGdvjYhhjbwMlNXRpzANaKeTlAsMagIqQihaHvyQZKnirlAHUvRUlocJcJKjfUGDSGwDg = -26447064.0f; NEonccRlBhkrhpGrXJoxmVsNGUmBCUGdvjYhhjbwMlNXRpzANaKeTlAsMagIqQihaHvyQZKnirlAHUvRUlocJcJKjfUGDSGwDg = wKuBjoQXwbAzksePoEbFXy; }
}

//Generated by cJunk
public class xkOlEEkPloxaXlXTBzMmfhKjucboeBUxIxMJmykLmgqLBJUtrVDbtlSQafqbdwlkUARZZNgDPAzzrzhkHhwJysDXfwjHIIiqvF
{
    void rBsuTdoAMtlTYaxFTvAbmbxuudeYuHRJElIgsMOSUygJlLXIKConpTTPExAqzPFEccPnSQMGh() { int iGnunIiFVOsSdTkRscuMXSFsbhJwkqtqhGH = -56810415; int MfVQoRkdANmyIeVZoRuJPBvSGzmVfVCkX = -65553293; iGnunIiFVOsSdTkRscuMXSFsbhJwkqtqhGH = -43296712; iGnunIiFVOsSdTkRscuMXSFsbhJwkqtqhGH = MfVQoRkdANmyIeVZoRuJPBvSGzmVfVCkX; }
}

//Generated by cJunk
public class MYlXLFvmpEfpiASOQFYnwsLkCKpePyfLBbwGjCcL
{
    void yXaxPcYzTGPPdwaTMBUBQQXrYUoHDMfNcMpYRNVZlpLIiPhEActiibviAjYuoQwHbBnRHBBzfuPbFOII() { bool njGaTwfBRHYipalVtDghcQxyauDnLUclaQHEJVnfWmzAdTJEJuhBibgXmIzZtrpxemIZTgLY = true; bool PBKpQwUHkyeQmbDzjjRBVOlexCVrLkkcjJ = false; njGaTwfBRHYipalVtDghcQxyauDnLUclaQHEJVnfWmzAdTJEJuhBibgXmIzZtrpxemIZTgLY = false; njGaTwfBRHYipalVtDghcQxyauDnLUclaQHEJVnfWmzAdTJEJuhBibgXmIzZtrpxemIZTgLY = PBKpQwUHkyeQmbDzjjRBVOlexCVrLkkcjJ; }
}

//Generated by cJunk
public class lgQqGqnspvNYKDiIpjOKCBPgsNadwzNCh
{
    void dpXjscbNdZqRYrSICvANZGkRkytPKvphLqhOLZwHmwZbYOTWIVtEMoIXEFyujMDijJGhKE() { string xdytachJsVeWHpBlUWiKJFaCXatqrPImLDXQFTNfpvNdPNEOgyojzAurCwWAjqRcNsKDRkgBICUOsxnKVeKtbYnLJgNJwNw = "zukWXFvGVlGhQHUhObpxrgdhIAkjrSFkbSDGMZu"; string ydlQXCAxwRmawffzFIMeEgYAoE = "hfqJusYsEJXSwLqNTQCOXULODgWFxEOUogHSAWjpkHfaUAIZlEkpJKXMUbGNVSuAuCd"; xdytachJsVeWHpBlUWiKJFaCXatqrPImLDXQFTNfpvNdPNEOgyojzAurCwWAjqRcNsKDRkgBICUOsxnKVeKtbYnLJgNJwNw = "QDYMwgNePoluDXxUtUYLdJvxZPVTRMffSWsVNrnIvgHXjgDqYyBgVrzGIanhKuNDIFcgfbGBgPPxBXOsLfuSKzJbX"; xdytachJsVeWHpBlUWiKJFaCXatqrPImLDXQFTNfpvNdPNEOgyojzAurCwWAjqRcNsKDRkgBICUOsxnKVeKtbYnLJgNJwNw = ydlQXCAxwRmawffzFIMeEgYAoE; }
}

//Generated by cJunk
public class lMmEFyBTDPdEXKxszzGbCFlLuVezutJvBJtOTxaRNdxIknoVwaKADzVgbbJkTBYKFhDOQiYCYoOYHdaGhJDUiIs
{
    void BnNmsnqyLFnCCkaEOBIcPlwZyADDJyDcxpBaDvfPhpCTUbYWGEpTZ() { Int32 gdQqoosjnpNBKENizBSWtuZidFWbhwEJkDQMkjPHmfuA = -14344198; Int32 oRJWLnGDWwgmDiNiRQCrkDQHnAyQmKjOzZtLvAQupBmoXeJCWgpVcCPlBCiKoqQMMRmdo = -34631644; gdQqoosjnpNBKENizBSWtuZidFWbhwEJkDQMkjPHmfuA = -95073961; gdQqoosjnpNBKENizBSWtuZidFWbhwEJkDQMkjPHmfuA = oRJWLnGDWwgmDiNiRQCrkDQHnAyQmKjOzZtLvAQupBmoXeJCWgpVcCPlBCiKoqQMMRmdo; }
}

//Generated by cJunk
public class BnRzICiRcjJqvuGVNlxJIKsRGFykabFrfOVsYWUAgTYSZTswK
{
    void qyfpTfquztCDyqBeRLhYNxrRwcomasmMCajcZCMieyYcRxidXoHILXwtHOtqqEUGGqAXGYEOzyuP() { float YOzyuLOmleBfeqVLJVsaeSWbxLWzjsJMdhUsOuwephVcpVtPDtFdFTkGsYBipPCsUoBwWcxOYNlwwtlbRGvqnfGJwNzpt = -86398347.0f; float NpZfdHBTwtawqFshoKymaQiTMFphszIdHVKYLQTmkHVNVuwaObIDACTwHFTXRTbxkxoVuTWXqMC = -25773976.0f; YOzyuLOmleBfeqVLJVsaeSWbxLWzjsJMdhUsOuwephVcpVtPDtFdFTkGsYBipPCsUoBwWcxOYNlwwtlbRGvqnfGJwNzpt = -85401982.0f; YOzyuLOmleBfeqVLJVsaeSWbxLWzjsJMdhUsOuwephVcpVtPDtFdFTkGsYBipPCsUoBwWcxOYNlwwtlbRGvqnfGJwNzpt = NpZfdHBTwtawqFshoKymaQiTMFphszIdHVKYLQTmkHVNVuwaObIDACTwHFTXRTbxkxoVuTWXqMC; }
}

//Generated by cJunk
public class SvUyRIPSQqFcECORFxvURgXdYFbfvFlN
{
    void oABBmHBPCBLGfVjTgCQZuskmYraMhjXKFsoEBMeyvgRQU() { int RxCVHzspLSvPOcuhphTdBBnhfSTYSdOaURGtXqZjeUtZsomjjQGyymTQKVqpgU = -33778037; int XhDVvxVBJyIjzgHNorfjoBXQcixqjBAjDKxLWWtoNiHIpOmnIxgyJwfVKmsMzfSHxYgpivqSyrVGuOaOvKhlVCZGebAjeg = -52645983; RxCVHzspLSvPOcuhphTdBBnhfSTYSdOaURGtXqZjeUtZsomjjQGyymTQKVqpgU = -67844330; RxCVHzspLSvPOcuhphTdBBnhfSTYSdOaURGtXqZjeUtZsomjjQGyymTQKVqpgU = XhDVvxVBJyIjzgHNorfjoBXQcixqjBAjDKxLWWtoNiHIpOmnIxgyJwfVKmsMzfSHxYgpivqSyrVGuOaOvKhlVCZGebAjeg; }
}

//Generated by cJunk
public class piQzKRhxENGOASbZbwLeRoxgmvyaKHuGnRBmpvEJ
{
    void hxGUfGBHeEAEXzPAlkMOSGmxSzaiQojgrCxTicBgurvGGpjnCBBFiqraKIBffSqpUT() { bool NVCwRIigUbLgqdQngPndxBKXIZaVAbAGlncNlGjsWCWPPeSYOsmbBrWMK = true; bool fnsiNfsaFvDBDCafvGuevHeHCGIoZEuQPCFSrJJXdp = false; NVCwRIigUbLgqdQngPndxBKXIZaVAbAGlncNlGjsWCWPPeSYOsmbBrWMK = false; NVCwRIigUbLgqdQngPndxBKXIZaVAbAGlncNlGjsWCWPPeSYOsmbBrWMK = fnsiNfsaFvDBDCafvGuevHeHCGIoZEuQPCFSrJJXdp; }
}

//Generated by cJunk
public class KiOXLqMvFkchPXIkfGWDAAAnlAwHolxWBFxvtevSceyKoimhKHeDAZoJiblKtFHTnF
{
    void gKZMrAKEbWCqyDNPwhHtVbQoNRskYXXeXiCcHxPdswySGrcixeIaMtjEtlzZHafDzY() { string TPBkHzReAZcyhWoKnuPwSdzxjWxpVSQvSiYMhMXIIhtJcuDdkxmloCFpQDBkBbJhJhgzx = "gyPkAIMZpabPovCURZCHdVvzWZJmJWUxhQcWwdhFIglIjAsbjhMBOOlKD"; string ccbXGokkpVpTUEXffGQciwdLnGgYLAuODfIVLNhNVFaiLQhXlCdaQWO = "PNayaPSFdiXTgXorwzxkmPVghDbKTjS"; TPBkHzReAZcyhWoKnuPwSdzxjWxpVSQvSiYMhMXIIhtJcuDdkxmloCFpQDBkBbJhJhgzx = "fSSgPGGiQSCGSUICKYJSPnaQAstRWoImuPsBNrYlpRhRkJCMZARmJRDsrBgXMpLbKPLCcGpSzYcyGIgqph"; TPBkHzReAZcyhWoKnuPwSdzxjWxpVSQvSiYMhMXIIhtJcuDdkxmloCFpQDBkBbJhJhgzx = ccbXGokkpVpTUEXffGQciwdLnGgYLAuODfIVLNhNVFaiLQhXlCdaQWO; }
}

//Generated by cJunk
public class rWXVccMzJLLBYzFFGUxzjTLzmloEliVphfypEpnjqcxdGyAdsACLspxYLXirnIOaqWTsA
{
    void QARhOJHVisnlLLlgJwsiadgGwviOWmkfHccAkeegpCdfuFcwcugZbKamTLl() { Int32 SbtzvIIdjjkvfSLsXABLqTllcGKExBpgZsL = -87478103; Int32 EwDhWmcsNsyAGPSClSAuzqHWpxEghtE = -13086182; SbtzvIIdjjkvfSLsXABLqTllcGKExBpgZsL = -41001953; SbtzvIIdjjkvfSLsXABLqTllcGKExBpgZsL = EwDhWmcsNsyAGPSClSAuzqHWpxEghtE; }
}

//Generated by cJunk
public class WnGHvUTfYOaYtdFDofyDOZxMOpgsYhvKTncNAHWiSmbgYROt
{
    void juPBjqpxpusGnaGqSPFFBrD() { float hIQlbxRnwanJeSubdxeYFPQusfkOIMIrrqisSEOzXHvfzRcYXobHWTyRrVtwNDekRvRnjBIYjeAyTkvaCLqUkBVYsQBxLS = -15273359.0f; float mUcLOEzppWfDXrSzyHaStB = -47187485.0f; hIQlbxRnwanJeSubdxeYFPQusfkOIMIrrqisSEOzXHvfzRcYXobHWTyRrVtwNDekRvRnjBIYjeAyTkvaCLqUkBVYsQBxLS = -60908978.0f; hIQlbxRnwanJeSubdxeYFPQusfkOIMIrrqisSEOzXHvfzRcYXobHWTyRrVtwNDekRvRnjBIYjeAyTkvaCLqUkBVYsQBxLS = mUcLOEzppWfDXrSzyHaStB; }
}

//Generated by cJunk
public class NJzQatuTALMRsgVHWraNWUphddFsRtXtY
{
    void lHSMxdGuVVUrGzmerkjrfetUSmxnCBHnSsHYCvYNUTAyVpXeSOaIoCtGOytKgSrU() { int OgYNsNbwMVWMduzcPSUobyzQzAKEBOhBbgkoRKmEmdwSfhcTzQvNxDtPgINrDXuxnVkx = -79692005; int KKLFgQCxBglRXqElTUDXnUIJwuBOjyfJwfJpFpghBvOAXwTMTIeTSMKpCCJmOnNCBbUshPgdoVWrGHmza = -78108514; OgYNsNbwMVWMduzcPSUobyzQzAKEBOhBbgkoRKmEmdwSfhcTzQvNxDtPgINrDXuxnVkx = -67682688; OgYNsNbwMVWMduzcPSUobyzQzAKEBOhBbgkoRKmEmdwSfhcTzQvNxDtPgINrDXuxnVkx = KKLFgQCxBglRXqElTUDXnUIJwuBOjyfJwfJpFpghBvOAXwTMTIeTSMKpCCJmOnNCBbUshPgdoVWrGHmza; }
}

//Generated by cJunk
public class ovbUMrkUUmDgtTNiLzvbbfDcBewqSjuPJMZsmipjkXMcdwuCJJ
{
    void vIqYzJryShvuClsnXhtUvcKMrDSazEkthJnmZkNQBoQHnqVbkpYMJoOTxd() { bool hvqQzNwBNtxFOnUcTHMCYAiq = true; bool qJgXksvUovDRakpCLmdbeeblsgVGGvEgMzaphZQUycRSjCCiSJrmXGMLhufEXoNtqWEaJ = false; hvqQzNwBNtxFOnUcTHMCYAiq = false; hvqQzNwBNtxFOnUcTHMCYAiq = qJgXksvUovDRakpCLmdbeeblsgVGGvEgMzaphZQUycRSjCCiSJrmXGMLhufEXoNtqWEaJ; }
}

//Generated by cJunk
public class zUmVIdBNJCpdnoxESIGmPHnuMxJPYbNxcYSpFQPdLyYspa
{
    void kAQoYNynushrJKTmphorBOVHy() { string BtdbRivElNJyjDzzodaINGEJYIrDxYVeIqWUVLdJlnXxJnGepsNkvzlupTDIrsHNrfptgfdcAQWZjGzkCNOELkiIEbfBIVStf = "HxgoFzbUexcuYBVmpPDobACfsPwhqPnPdJygQYrKnPFiARngyfQOaNaGuBwpCuePJNQOCoVRXtALuUfbbQiKzZpP"; string KFexMxKPkkfdLLiUSFedCB = "jrmPcCHtlqYnAYdKJyqgNOMaVUNmLTPkXBfLCGpVmHZEwsaCmYBAiICUfMtHoUiRanPytSIDJVuFo"; BtdbRivElNJyjDzzodaINGEJYIrDxYVeIqWUVLdJlnXxJnGepsNkvzlupTDIrsHNrfptgfdcAQWZjGzkCNOELkiIEbfBIVStf = "IdduKAnSOBOzLfqMhgRYHdIcbJTQxrNOwfXiPMeTvzMlAzhnRqbochgeVATAhXUXoADgHIwVCydIRWccbfyHPZ"; BtdbRivElNJyjDzzodaINGEJYIrDxYVeIqWUVLdJlnXxJnGepsNkvzlupTDIrsHNrfptgfdcAQWZjGzkCNOELkiIEbfBIVStf = KFexMxKPkkfdLLiUSFedCB; }
}

//Generated by cJunk
public class mOysNPtdmYfoxAvBfJuGleDYNyYqZBYNHPOohPvhbZLFdcZbgazo
{
    void NJoHLZeOrCEkzSgimGTQOMeLSyvlZwJyCjAyaGdHkgqZVbFLvqpaGiaAecoMetousFiP() { Int32 RiHfdLghWFpqtkpiGjsTJzTyRRdqrvgQtptNjJwuCsdZnxTtDGnJNOGSOJOLipqbZZxDrbzxmxQ = -98714924; Int32 QWtKUxjmlHXDejbyZvlMMCCAOAQCcGqWPypvUytNyDETCVS = -4692841; RiHfdLghWFpqtkpiGjsTJzTyRRdqrvgQtptNjJwuCsdZnxTtDGnJNOGSOJOLipqbZZxDrbzxmxQ = -98446706; RiHfdLghWFpqtkpiGjsTJzTyRRdqrvgQtptNjJwuCsdZnxTtDGnJNOGSOJOLipqbZZxDrbzxmxQ = QWtKUxjmlHXDejbyZvlMMCCAOAQCcGqWPypvUytNyDETCVS; }
}

//Generated by cJunk
public class TQdZBBlEOTtwThhKfRBogtfdaBXNJyjyshwmygoUdtbbbCag
{
    void RsZBBhlZlzyAeYvQkvDlluMycgKnSDCYbrvRZhxKvWBzhopFyLrfyouWgaZU() { float XyHhwaYncdrDytpOtMRorFoHMXZmwYkyHcnxOTCHPBcUUngFyfdTJlcRKAVZAmrEtsvVFs = -66752199.0f; float zaJJYpdTmYqUoQvRpWAVnOydGdKXbbs = -19957789.0f; XyHhwaYncdrDytpOtMRorFoHMXZmwYkyHcnxOTCHPBcUUngFyfdTJlcRKAVZAmrEtsvVFs = -17473375.0f; XyHhwaYncdrDytpOtMRorFoHMXZmwYkyHcnxOTCHPBcUUngFyfdTJlcRKAVZAmrEtsvVFs = zaJJYpdTmYqUoQvRpWAVnOydGdKXbbs; }
}

//Generated by cJunk
public class LAFfCKzMnoqNwigxaIbCYVkFlbgDvfFsLgfTiksHpKMgxBBuZfj
{
    void PzYTpQAubdvRIjxCQDMPpQUMOJDIkaldPvqsmMpDhqRdutExzykrNQtzeLFgMsKAsSBNsEQEOIh() { int zyrSUEFIlquQzBrOaGJqpgRkbrrFuifcEiuAmtkbHjYJyBCLBqpxERsThHrdNAOCKRLRHq = -25754577; int bhWhjFHHmZpfDDtiXmpaTfBHOHVWzgqlEKPIbXimMZVRnhwKJctZbgXvfOSbkkHFjDatokHAEDO = -75503886; zyrSUEFIlquQzBrOaGJqpgRkbrrFuifcEiuAmtkbHjYJyBCLBqpxERsThHrdNAOCKRLRHq = -66528024; zyrSUEFIlquQzBrOaGJqpgRkbrrFuifcEiuAmtkbHjYJyBCLBqpxERsThHrdNAOCKRLRHq = bhWhjFHHmZpfDDtiXmpaTfBHOHVWzgqlEKPIbXimMZVRnhwKJctZbgXvfOSbkkHFjDatokHAEDO; }
}

//Generated by cJunk
public class tKkTgLKNXnRKYnUdMFGBbRPhjHCVY
{
    void UtUYRdkuumQZXkRStCFUGuvxBmcuDjZIpo() { bool CuwlGKjiRSqcEhxPpFwBRaxXXeAprKMHkjiXWIvvlYccGZivxnuULnWbwYOcJtrH = true; bool GirVzashKBQtyVQxwFsDQswxMGbxtTlrgVVYhMvWvMKKXcdaIQfDAnqNmuIsyuoqP = false; CuwlGKjiRSqcEhxPpFwBRaxXXeAprKMHkjiXWIvvlYccGZivxnuULnWbwYOcJtrH = false; CuwlGKjiRSqcEhxPpFwBRaxXXeAprKMHkjiXWIvvlYccGZivxnuULnWbwYOcJtrH = GirVzashKBQtyVQxwFsDQswxMGbxtTlrgVVYhMvWvMKKXcdaIQfDAnqNmuIsyuoqP; }
}