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

[GET]api/chat/room/{id} Возвращает вообще все сообщения комнаты
[GET]api/chat/{pageIndex}/{pageSize}/{chatRoomId} Возвращает сообщения комнаты постранично
[GET]api/chat/{id} Возвращает одно сообщение
[PUT]api/chat/{id} Редактирует сообщение. Тело: chatMessageDto
[POST]api/chat Создает сообщение. Тело: chatMessageDto
[DELETE]api/chat/{id} Удаляет сообщение

[GET]api/room Возвращает все комнаты
[GET]api/room/{id} Возвращает одну комнату
[GET]api/room/user/{userId} Возвращает комнаты пользователя
[GET]api/room/{id}/users Возвращает пользователей добавленных в комнату
[POST]api/room Создает комнату. Тело chatRoomDto
[PUT]api/room/{id} Редактирует комнату. Тело chatRoomDto
[DELETE]api/room{id} Удаляет комнату
[POST]api/add-user/{chatRoomId}/{userId} Добавляет пользователя к комнате
[DELETE]api/remove-user/{chatRoomId}/{userId} Удаляет пользователя из комнаты

Путь к хабу hubs/chat
receiveMessage ваш метод для получения сообщений. Тело: messageDto