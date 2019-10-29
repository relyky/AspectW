# 未來語法想像
## 語法結構
提供新關鍵字[wave]做程式碼交織。語去靈感來自"using"關鍵字。
    
	wave(Aspect,[params],[lamda])	
	{
		...code section...
	}
  

## 語法想像一  
    
    [Authorize] //Attribute仍支援靜態的屬性
    JsonResult DoSomething(string param1,...)
    {
		wave(Trace, traceHandler) // 提供新關鍵字[wave]做程式碼交織。
		wave(IgnoreIfFail)
		{  // 用大括號"{}"圈定欲交織的程式碼區塊
			...code section 1...  
		}
		
		wave(Trace, traceHandler)
		wave(Timer, (beginTime, endTime)=>{...}) // 可即時取得新狀態做交易執行
		wave(Retry, 2)
		{
			...code section 2... 在一演算程序中，可有多個程式碼節段
		}
    }
    
## 語法想像二  
  
    [Authorize] //Attribute仍支援靜態的屬性
    JsonResult DoSomething(string param1,...)
    {
    	wave(Trace, traceHandler) // 提供新關鍵字[wave]做程式碼交織。
    	wave(When, param1 != 'STOP')
    	{
    		wave(IgnoreIfFail) // 支援巢狀
    		{  // 用大括號"{}"圈定欲交織的程式碼區塊
    			... setp 1 code section ...  
    		}
    		
    		wave(Timer, timerHandler)
    		wave(RestoreIfFail)
    		wave(Retry, 2)
    		{
    			... setp 2 code section ... 在一演算程序中，可有多個程式碼節段
    			... 執行交易
    		}
    	}
    
    }
