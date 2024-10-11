using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Camera_Vision
{
    class BaslerGigE
    {
        public delegate void OnImageGrabbedEventDelegate(IGrabResult ptrGrabResult, int nCamera);
        public OnImageGrabbedEventDelegate OnImageGrabbedCallback;

        public delegate void OnConnectionLostEventDelegate(int nCamera);
        public OnConnectionLostEventDelegate OnConnectionLostCallback;

        public delegate void OnCameraExceptionEventDelegate(int nCamera);
        public OnCameraExceptionEventDelegate OnCameraExceptionCallback;

        private static List<ICameraInfo> m_DeviceList = new List<ICameraInfo>();
        private PixelDataConverter       m_Converter  = new PixelDataConverter();

        private Camera   m_Basler = null;
        private int      m_nCamera;
        private eTRIGGER m_eMode;

        public BaslerGigE()
        {
            m_nCamera = -1;
            m_eMode   = eTRIGGER.eMODE_SOFTTRIGGER;
        }

        public void Discovery()
        {
            try
            {
                List<ICameraInfo> DeviceList = CameraFinder.Enumerate();
                m_DeviceList = DeviceList;

                for (int i = 0; i < DeviceList.Count; i++)
                {
                    ICameraInfo CameraInfo = DeviceList[i];

                    string sFullName = CameraInfo[CameraInfoKey.FullName];
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void ShowException(Exception ex)
        {
            if (OnCameraExceptionCallback != null)
            {
                OnCameraExceptionCallback(m_nCamera);
            }

            MessageBox.Show("BaslerGigE Exception:\n" + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public int SearchDevice(string sIpAddress)
        {
            try
            {
                if (m_Basler == null)
                {
                    for (int i = 0; i < m_DeviceList.Count; i++)
                    {
                        if (m_DeviceList[i][CameraInfoKey.DeviceIpAddress] == sIpAddress)
                        {
                            return i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return -1;
        }

        public bool OpenByIP(string ipAddress, int nCamera)
        {
            try
            {
                m_nCamera = nCamera;
                return OpenByIndex(SearchDevice(ipAddress));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return false;
        }

        public bool OpenByIndex(int nIndex)
        {
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "10000" /*ms*/);
            List<ICameraInfo> CameraInfo = CameraFinder.Enumerate(DeviceType.GigE);

            try
            {
                if (m_Basler == null)
                {
                    m_Basler = new Camera(m_DeviceList[nIndex]);
                    m_Basler.ConnectionLost             += OnConnectionLost;
                    m_Basler.CameraOpened               += OnCameraOpened;
                    m_Basler.CameraClosed               += OnCameraClosed;
                    m_Basler.StreamGrabber.GrabStarted  += OnGrabStarted;
                    m_Basler.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                    m_Basler.StreamGrabber.GrabStopped  += OnGrabStopped;

                    m_Basler.Open();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool GrabStart()
        {
            try
            {
                if (m_Basler != null)
                {
                    if (m_Basler.IsOpen)
                    {
                        if (!m_Basler.StreamGrabber.IsGrabbing)
                        {
                            m_Basler.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return false;
        }
        public bool GrabStop()
        {
            try
            {
                if (m_Basler != null)
                {
                    if (m_Basler.IsOpen)
                    {
                        if (m_Basler.StreamGrabber.IsGrabbing)
                        {
                            m_Basler.StreamGrabber.Stop();
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return false;
        }

        public void Close()
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Close();
                    m_Basler = null;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool IsOpen()
        {
            try
            {
                if (m_Basler != null)
                {
                    return m_Basler.IsOpen;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return false;
        }

        public bool IsGrabbing()
        {
            try
            {
                if (m_Basler != null && m_Basler.IsOpen)
                {
                    return m_Basler.StreamGrabber.IsGrabbing;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return false;
        }

        public bool IsColorCamera()
        {
            bool result = false;

            try
            {
                if (m_Basler.Parameters[PLCamera.PixelFormat].GetValue().Contains("Bayer"))
                {
                    result = true;
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        public void SetTriggerStatus(eTRIGGER eMode)
        {
            try
            {
                switch (eMode)
                {
                    case eTRIGGER.eMODE_SOFTTRIGGER:
                        {
                            m_Basler.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                            m_Basler.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Software);
                        }
                        break;

                    case eTRIGGER.eMODE_HARDTRIGGER:
                        {
                            m_Basler.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                            m_Basler.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line1);
                            m_Basler.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.FallingEdge);
                        }
                        break;

                    case eTRIGGER.eMODE_CONTINUE:
                        {
                            m_Basler.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                            m_Basler.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
                        }
                        break;
                }

                m_eMode = eMode;
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public eTRIGGER GetTriggerStatus()
        {
            eTRIGGER eMode = eTRIGGER.eMODE_NONE;

            try
            {
                string sMode = m_Basler.Parameters[PLCamera.TriggerMode].GetValue();
                switch (sMode)
                {
                    case "On":
                        {
                            string sSource = m_Basler.Parameters[PLCamera.TriggerSource].GetValue();
                            switch (sSource)
                            {
                                case "Software":
                                    {
                                        eMode = eTRIGGER.eMODE_SOFTTRIGGER;
                                    }
                                    break;

                                case "Line1":
                                    {
                                        eMode = eTRIGGER.eMODE_HARDTRIGGER;
                                    }
                                    break;
                            }
                        }
                        break;

                    case "Off":
                        {
                            eMode = eTRIGGER.eMODE_CONTINUE;
                        }
                        break;
                }

                m_eMode = eMode;
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }

            return eMode;
        }
        public void GenerateSoftTrigger()
        {
            try
            {
                if (m_Basler != null)
                {
                    if (m_Basler.IsOpen)
                    {
                        m_Basler.ExecuteSoftwareTrigger();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        string GetModelName()
        {
            return m_Basler.Parameters[PLCamera.DeviceModelName].GetValue();
        }

        string GetSerialNumber()
        {
            return m_Basler.Parameters[PLCamera.DeviceSerialNumber].GetValue();
        }

        public long GetWidth()
        {
            return m_Basler.Parameters[PLCamera.Width].GetValue();
        }

        public long GetHeight()
        {
            return m_Basler.Parameters[PLCamera.Height].GetValue();
        }

        public long GetGain()
        {
            return m_Basler.Parameters[PLCamera.GainRaw].GetValue();
        }

        public void SetGain(long lValue)
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off);
                    m_Basler.Parameters[PLCamera.GainSelector].TrySetValue(PLCamera.GainSelector.All);
                    m_Basler.Parameters[PLCamera.GainRaw].TrySetValue(lValue);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public double GetGamma()
        {
            return m_Basler.Parameters[PLCamera.Gamma].GetValue();
        }

        public void SetGamma(double dValue)
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.GammaEnable].TrySetValue(true);
                    m_Basler.Parameters[PLCamera.Gamma].TrySetValue(dValue);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public long GetExposureTime()
        {
            return m_Basler.Parameters[PLCamera.ExposureTimeRaw].GetValue();
        }

        public void SetExposureTime(long lValue)
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off);
                    m_Basler.Parameters[PLCamera.ExposureMode].TrySetValue(PLCamera.ExposureMode.Timed);
                    m_Basler.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(lValue);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void SetAOI(long lOffsetX, long lOffsetY, long lWidth, long lHeight)
        {
            try
            {
                if (m_Basler != null)
                {
                    if (m_Basler.IsOpen)
                    {
                        bool bGrabbing = m_Basler.StreamGrabber.IsGrabbing;
                        if (bGrabbing)
                        {
                            m_Basler.StreamGrabber.Stop();
                        }

                        m_Basler.Parameters[PLCamera.OffsetX].TrySetValue(lOffsetX);
                        m_Basler.Parameters[PLCamera.OffsetY].TrySetValue(lOffsetY);
                        m_Basler.Parameters[PLCamera.Width].TrySetValue(lWidth);
                        m_Basler.Parameters[PLCamera.Height].TrySetValue(lHeight);
                        m_Basler.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);

                        if (bGrabbing)
                        {
                            m_Basler.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void SetPacketSize(long lValue)
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.PacketSize].TrySetValue(lValue);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void SetWhiteBalance()
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Continuous);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        public void SetGrabDelay(double dValue)
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.TriggerDelayAbs].SetValue(dValue);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void SetDebouncerTime(double dValue)
        {
            try
            {
                if (m_Basler != null)
                {
                    m_Basler.Parameters[PLCamera.LineDebouncerTimeAbs].SetValue(dValue);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void OnConnectionLost(Object sender, EventArgs e)
        {
            try
            {
                if (m_Basler != null)
                {
                    OnConnectionLostEvent(m_nCamera);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void OnCameraOpened(Object sender, EventArgs e)
        {
            Debug.WriteLine("OnCameraOpened");
        }

        private void OnCameraClosed(Object sender, EventArgs e)
        {
            Debug.WriteLine("OnCameraClosed");

        }

        private void OnGrabStarted(Object sender, EventArgs e)
        {
            Debug.WriteLine("OnGrabStarted");
        }

        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                if (m_Basler != null)
                {
                    IGrabResult grabResult = e.GrabResult;
                    using (grabResult)
                    {
                        if (grabResult.GrabSucceeded)
                        {
                            OnImageGrabbedEvent(grabResult, m_nCamera);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                e.DisposeGrabResultIfClone();
            }
        }

        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            Debug.WriteLine("OnGrabStopped");
        }

        public void OnConnectionLostEvent(int nCamera)
        {
            if (OnConnectionLostCallback != null)
            {
                OnConnectionLostCallback(nCamera);
            }
        }

        public void OnImageGrabbedEvent(IGrabResult ptrGrabResult, int nCamera)
        {
            if (OnImageGrabbedCallback != null)
            {
                OnImageGrabbedCallback(ptrGrabResult, nCamera);
            }
        }
    }
}
