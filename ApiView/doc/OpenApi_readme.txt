OpenApi使用说明文档
1.引用BSF.dll和BSF.BaseService.OpenApi.dll
2.在MVC网站Controller下面的接口方法处添加OpenDocAttribute特性注释即可。
3.参考ApiViewTestWeb使用方式中的DemoController。
4.在AppView站点中发布OpenApi的信息。
  配置AppView中web.config;配置如下：
  <!--多个Api文档可以配置多个DllPath,如DllPath1,DllPath2;
      接口dll路径 api程序集路径,多个;分隔 格式：需要公开接口的dll路径;接口名;测试的appurl'-->
    <add key="DllPath1" value="D:\svn-working\RT_Cloud\trunk\源代码\BaseService\ApiView\ApiViewTestWeb\bin\ApiViewTestWeb.dll;ApiViewTestWeb【示例】;http://10.17.72.96:8081/"/>
    <add key="DllPath2" value="D:\svn-working\RT_Cloud\trunk\源代码\BaseService\ApiView\ApiViewTestWeb\bin\demo.dll;demo【示例】;http://10.17.72.96:8082/"/>
5.点击AppView站点“帮助文档”中“重新加载”按钮生效。