messageDto:
{
  "id": '',
  "creationDate": '',
  "chatRoomId": '',
  "senderId": '',
  "text": ''
}

roomDto:
{
  "id": '',
  "creationDate": '',
  "name": ''
}

[GET]api/chat/room/{id} ���������� ������ ��� ��������� �������
[GET]api/chat/{pageIndex}/{pageSize}/{chatRoomId} ���������� ��������� ������� �����������
[GET]api/chat/{id} ���������� ���� ���������
[PUT]api/chat/{id} ����������� ���������. ����: chatMessageDto
[POST]api/chat ������� ���������. ����: chatMessageDto
[DELETE]api/chat/{id} ������� ���������

[GET]api/room ���������� ��� �������
[GET]api/room/{id} ���������� ���� �������
[GET]api/room/user/{userId} ���������� ������� ������������
[GET]api/room/{id}/users ���������� ������������� ����������� � �������
[POST]api/room ������� �������. ���� chatRoomDto
[PUT]api/room/{id} ����������� �������. ���� chatRoomDto
[DELETE]api/room{id} ������� �������
[POST]api/add-user/{chatRoomId}/{userId} ��������� ������������ � �������
[DELETE]api/remove-user/{chatRoomId}/{userId} ������� ������������ �� �������

���� � ���� hubs/chat
receiveMessage ��� ����� ��� ��������� ���������. ����: messageDto