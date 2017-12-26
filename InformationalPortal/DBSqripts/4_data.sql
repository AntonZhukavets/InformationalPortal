insert into Roles(roleName, roleDesc)
values('Administrator','can manage all data of portal')
insert into Roles(roleName, roleDesc)
values('user','can manage his own data')
insert into Users(firstName, lastName, login, password, email, isBlocked)
values('Nick', 'Maersk', 'nikcMaersk','123456','nickmaersk@gmail.com',0)
insert into Users(firstName, lastName, login, password, email, isBlocked)
values('Nataly', 'Maersk', 'nataMaersk','123456', 'natamaersk@gmail.com',0)
insert into Users(firstName, lastName, login, password, email, isBlocked)
values('Anton', 'Zhukavets', 'AntonZhukavets','pa$$word', 'golfgtiw120@yandex.ru',0)
insert into Users(firstName, lastName, login, password, email, isBlocked)
values('Admin', 'Admin', 'admin','admin', 'admin@yandex.ru',0)

insert into UsersRoles(userId,roleId)
values(1,2)
insert into UsersRoles(userId,roleId)
values(2,2)
insert into UsersRoles(userId,roleId)
values(3,1)
insert into UsersRoles(userId,roleId)
values(4,1)

insert into Languages(languageName,deleted)
values('English',0)
insert into Languages(languageName,deleted)
values('Deutsch',0)
insert into Languages(languageName,deleted)
values('Russian',0)
insert into Languages(languageName,deleted)
values('Franch',0)
insert into Languages(languageName,deleted)
values('Italian',0)
insert into Languages(languageName,deleted)
values('Spanish',0)

insert into Articles(name, pictureLink, userId)
values('All about France','pictureLink',2)
insert into Info(id,languageId,date,text,videoLink)
values(1,1,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink,userId)
values('All about England','pictureLink',3)
insert into Info(id,languageId,date,text,videoLink)
values(2,1,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('All about Spain','pictureLink',2)
insert into Info(id,languageId,date,text,videoLink)
values(3,2,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('Munich bmw museum','pictureLink',3)
insert into Info(id,languageId,date,text,videoLink)
values(4,2,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('Have a nice Hollydays in Austria','pictureLink',1)
insert into Info(id,languageId,date,text,videoLink)
values(5,3,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('Old e30 M3 better then new M4?','pictureLink',2)
insert into Info(id,languageId,date,text,videoLink)
values(6,3,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('The history of Mercedes','pictureLink',1)
insert into Info(id,languageId,date,text,videoLink)
values(7,1,'11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink,userId)
values('New type of engines','pictureLink',2)
insert into Info(id,languageId,date,text,videoLink)
values(8,1,'11.11.2017','some text','videoLink')

insert into Headings(name, description, deleted)
values('Travelling', 'All about travelling', 0)

insert into Headings(name, description, deleted)
values('Sport', 'All about sport', 0)

insert into Headings(name, description, deleted)
values('Home', 'All about home', 0)

insert into Headings(name, description, deleted)
values('Cars', 'All about cars', 0)

insert into Headings(name, description, deleted)
values('Cinema', 'All about cinema', 0)

insert into Headings(name, description, deleted)
values('News', 'All about news', 0)

insert into Headings(name, description, deleted)
values('Culture', 'All about culture', 0)


insert into ArticlesHeadings(ArticleId,HeadingId)
values(1,3)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(1,2)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(1,5)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(2,3)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(2,2)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(2,1)


insert into ArticlesHeadings(ArticleId,HeadingId)
values(3,3)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(3,2)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(3,7)


insert into ArticlesHeadings(ArticleId,HeadingId)
values(4,3)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(4,2)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(4,7)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(5,2)


insert into ArticlesHeadings(ArticleId,HeadingId)
values(5,6)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(6,3)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(6,7)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(7,6)


insert into ArticlesHeadings(ArticleId,HeadingId)
values(7,2)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(8,6)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(8,2)

insert into ArticlesHeadings(ArticleId,HeadingId)
values(8,7)

