OpenApiʹ��˵���ĵ�
1.����BSF.dll��BSF.BaseService.OpenApi.dll
2.��MVC��վController����Ľӿڷ��������OpenDocAttribute����ע�ͼ��ɡ�
3.�ο�ApiViewTestWebʹ�÷�ʽ�е�DemoController��
4.��AppViewվ���з���OpenApi����Ϣ��
  ����AppView��web.config;�������£�
  <!--���Api�ĵ��������ö��DllPath,��DllPath1,DllPath2;
      �ӿ�dll·�� api����·��,���;�ָ� ��ʽ����Ҫ�����ӿڵ�dll·��;�ӿ���;���Ե�appurl'-->
    <add key="DllPath1" value="D:\svn-working\RT_Cloud\trunk\Դ����\BaseService\ApiView\ApiViewTestWeb\bin\ApiViewTestWeb.dll;ApiViewTestWeb��ʾ����;http://10.17.72.96:8081/"/>
    <add key="DllPath2" value="D:\svn-working\RT_Cloud\trunk\Դ����\BaseService\ApiView\ApiViewTestWeb\bin\demo.dll;demo��ʾ����;http://10.17.72.96:8082/"/>
5.���AppViewվ�㡰�����ĵ����С����¼��ء���ť��Ч��