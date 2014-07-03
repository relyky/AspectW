AspectW：剖面導向之例外處理與重試機制

摘  要

雖然絕大多數的現代程式語言都有嚐試與補獲（try-and-catch）的例外處理機制，提供開發人員撰寫模組性高的例外處理程式碼，既可以立即處理例外狀況，也可以將例外傳遞（propagate）到系統其他模組。但是很多例外情況是暫態的（transient），發生後，應用程式是有可能從例外狀況恢復（recovery）過來，而不必啟動例外處理的程序。例如：分散式系統在進行網路連線時，可能因為網路一時不穩定而失敗，但稍停幾秒後再進行連線就可以了。於是就有學者倡議擴充例外處理機制，增加補獲與重試（catch-and-retry）的功能。本研究採用剖面導向（aspect-oriented）的觀點，參考AspectF的方法來實作一個輕量級的補獲與重試模組，AspectW，讓開發人員可以“流利（fluent）介面”的方式輕鬆撰寫例外捕獲與重試的程式碼，達到了讓開發人員不必更動主功能邏輯程式碼就能簡單就能加入補獲與重試的功能。

關鍵詞―Exception, Exception handling, Try-and-Catch、AOP、Catch-and-Retry

======
AspectW: an Aspect-Oriented Catch and Retry Mechanism

Abstract

Most modern programming languages support try-and-catch mechanism that enables developers to write modular exception handling code which can not only process exceptions immediately but also propagate them to the other modules in the system. However, many exceptions are transient, when occurred, the application is likely to recover from the exception, thereby rendering it unnecessary to start up the exception handling code. For example: during a network connection in a distributed system, you may fail due to network instability moment, suffice to wait for a few seconds and then re-connect it. Thus, some scholars initiate to expand the exception handling mechanism, and proposed catch-and-retry mechanism. This thesis takes an aspect-oriented point of view, and adapts the AspectF library to implement a lightweight level catch-and-retry library, AspectW, so that developers can use the “fluent interface” approach to easily write exceptions capture and retry code. As a result, developers do not have to modify the main logic code and could simply add catch-and-retry code to handle exceptions well.

Keywords: Exception, Exception handling, Try-and-Catch, AOP, Catch-and-Retry
