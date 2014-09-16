using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeyboardFormatterApp
{
    public class KeyboardHelper
    {
        private int keyboardTextIndex;
        public string KeyboardText { get; set; }
        public string KeyboardFunctionText { get; set; }
        private bool isShift;
        public bool IsCtrlPressed { get; set; }
        private bool isCapsLock;
        
        private Dictionary<int, string> functionDictionary;

        public KeyboardHelper()
        {
            functionDictionary = new Dictionary<int, string>();
            RegisterFunctionKeys();
            isShift = false;
            IsCtrlPressed = false;
            isCapsLock = false;
            KeyboardText = "";
        }

        public void StartKeyboardText(KeyEventArgs e)
        {
            keyboardTextIndex = 0;
            AddCharKey(e);
        }


        public void ManageKeysOnUp(KeyEventArgs e)
        {
             if (e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.LShiftKey)
            {
                isShift = false;
            }
            else if (e.KeyValue == 162)
            {

                IsCtrlPressed = false;
                if (KeyboardFunctionText == functionDictionary[162])
                {
                    //resetting functionTest since didn't do anything with ctrl
                    KeyboardFunctionText = "";
                }
            }
        }

        public void ManageKeysOnDown(KeyEventArgs e)
        {
            //if ((e.KeyValue >= 112 && e.KeyValue <= 124) || e.KeyValue == 13)
            //{
            //    CreateKeyboardFunctionText(e);
            //}
            if ((e.KeyValue >= 65 && e.KeyValue <= 90) || (e.KeyValue >= 48 && e.KeyValue <= 57)
                || (e.KeyValue >= 96 && e.KeyValue <= 105))
            {
                if (KeyboardText == "" && KeyboardFunctionText == "")
                {
                    StartKeyboardText(e);
                }
                else
                {
                    AddCharKey(e);
                }
             
            }
            else
            {
                ManageNonCharKeys(e);
            }

        }

        private void ManageNonCharKeys(KeyEventArgs e)
        {

            if (e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.LShiftKey)
            {
                isShift = true;
            }
            if (e.KeyValue == 162)
            {
                IsCtrlPressed = true;
                CreateKeyboardFunctionText(e);
            }
            else
            {
                char keyChar = Convert.ToChar(e.KeyValue);
                //left
                    if ((e.KeyValue == 37))
                    {
                        if (keyboardTextIndex > 0)
                        {
                            keyboardTextIndex -= 1;
                        }
                    }
                    else if (e.KeyValue == 191)
                    {
                        if (isShift)
                        {
                            InsertKeyChar("?");
                        }
                        else
                        {
                            InsertKeyChar("/");
                        }

                        keyboardTextIndex++;
                    }
                    else if (e.KeyValue == 220)
                    {
                        if (isShift)
                        {
                            InsertKeyChar("|");
                        }
                        else
                        {
                            InsertKeyChar("\\");
                        }

                        keyboardTextIndex++;
                    }
                        //right
                    else if (e.KeyValue == 39)
                    {
                        if (keyboardTextIndex > 0)
                        {
                            keyboardTextIndex += 1;
                        }
                    }
                        //space
                    else if (e.KeyValue == 32)
                    {
                        InsertKeyChar(" ");
                        keyboardTextIndex++;
                    }
                    else if (e.KeyValue == 8)
                    {
                        KeyboardText = KeyboardText.Remove(keyboardTextIndex - 1, 1);
                        keyboardTextIndex--;
                    }
                    else if (keyChar == '½')
                    {
                        if (isShift)
                        {
                            InsertKeyChar("_");
                        }
                        else
                        {
                            InsertKeyChar("-");
                        }

                        keyboardTextIndex++;
                    }
                   


                }
            }
        
            private void AddCharKey (KeyEventArgs e)
            {
                if (IsCtrlPressed)
                {
                    ManageCtrlPlusChar(e);
                }
                else
                {
                    char keyChar = Convert.ToChar(e.KeyValue);
                    InsertKeyChar( keyChar.ToString());
                    keyboardTextIndex++;
                }
       
            }
        
            //adding character key to ctrl function text, "{Ctrl} + C", etc
            private void ManageCtrlPlusChar(KeyEventArgs e)
            {
                char keyChar = Convert.ToChar(e.KeyValue);
                KeyboardFunctionText += keyChar;
            }

            public void CreateKeyboardFunctionText (KeyEventArgs keyEventArgs)
            {
                KeyboardFunctionText = functionDictionary[keyEventArgs.KeyValue];
                
            }

            public void ResetKeyboardText()
            {
                KeyboardText = "";
                keyboardTextIndex = 0;
            }

            public bool IsFunctionKey(KeyEventArgs e)
            {
                if ((e.KeyValue >= 112 && e.KeyValue <= 127 || e.KeyValue == 13))
                    return true;
                return false;
            }

            public void IsCtrlKeyPressed(KeyEventArgs e)
            {
                if (e.KeyValue == 162 && KeyboardFunctionText != "" && KeyboardFunctionText!= null)
                {
                    IsCtrlPressed = true;
                }
                IsCtrlPressed = false;
            }


            public void RegisterFunctionKeys ()
            {
                functionDictionary.Add(112, "{F1}");
                functionDictionary.Add(113, "{F2}");
                functionDictionary.Add(114, "{F3}");
                functionDictionary.Add(115, "{F4}");
                functionDictionary.Add(116, "{F5}");
                functionDictionary.Add(117, "{F6}");
                functionDictionary.Add(118, "{F7}");
                functionDictionary.Add(119, "{F8}");
                functionDictionary.Add(120, "{F9}");
                functionDictionary.Add(121, "{F10}");
                functionDictionary.Add(122, "{F11}");
                functionDictionary.Add(123, "{F12}");
                functionDictionary.Add(13, "{ENTER}");
                functionDictionary.Add(162, "{CTRL} + ");
            }



        private void InsertKeyChar (string key)
            {
                if (!isShift) //lowercase
                {
                    KeyboardText = KeyboardText.Insert(keyboardTextIndex, key.ToLower());
                }
                else if (isShift) //uppercase
                {
                    KeyboardText = KeyboardText.Insert(keyboardTextIndex, key.ToUpper());
                }
            }
        }
    }