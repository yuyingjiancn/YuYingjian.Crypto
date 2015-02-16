##YuYingjian.Crypto
.Net的加密库0.0.2版。

visual studio 2013编写 .net framework 4.5.1

###Example
```cs
using YuYingjian.Crypto;

"str".ToBASE64().FromBASE64();
"str".ToMD5();
"str".ToSHA1();

//下面的代码用于自己设定key和iv
var key = "虞颖健在海宁一中工作，是高中信息技术教师";
var iv = "虞颖健在海宁一中工作";
DESCrypto.SetKeyAndIV(key=key, iv=iv); //key必须能转成至少8字节，iv8字节
AESCrypto.SetKeyAndIV(key=key, iv=iv); //key必须能转成至少32字节，iv16字节

"虞颖健".ToDES().ToAES().FromAES().FromDES(); //虞颖健

"haha".To3DES().From3DES();
```

###NuGet
PM> Install-Package YuYingjian.Crypto
