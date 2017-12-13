insert into Roles(roleName, roleDesc)
values('admin','can manage all data of portal')
insert into Roles(roleName, roleDesc)
values('user','can manage his own data')

insert into Users(firstName, lastName, age, login, password, email, roleId)
values('Nick', 'Maersk', 42, 'nikcMaersk','123456', 'nickmaersk@gmail.com',2)
insert into Users(firstName, lastName, age, login, password, email, roleId)
values('Nataly', 'Maersk', 37, 'nataMaersk','123456', 'natamaersk@gmail.com',2)
insert into Users(firstName, lastName, age, login, password, email, roleId)
values('Andrew', 'Rinsky', 25, 'andyR','123456', 'andrewRin@gmail.com',2)

insert into Articles(name, pictureLink, userId)
values('All about France','pictureLink',2)
insert into Info(id,language,date,text,videoLink)
values(1,'Deutsch','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink,userId)
values('All about England','pictureLink',3)
insert into Info(id,language,date,text,videoLink)
values(2,'English','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('All about Spain','pictureLink',2)
insert into Info(id,language,date,text,videoLink)
values(3,'English','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('Munich bmw museum','pictureLink',3)
insert into Info(id,language,date,text,videoLink)
values(4,'English','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('Have a nice Hollydays in Austria','pictureLink',1)
insert into Info(id,language,date,text,videoLink)
values(5,'English','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('Old e30 M3 better then new M4?','pictureLink',2)
insert into Info(id,language,date,text,videoLink)
values(6,'English','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink, userId)
values('The history of Mercedes','pictureLink',1)
insert into Info(id,language,date,text,videoLink)
values(7,'English','11.11.2017','some text','videoLink')

insert into Articles(name, pictureLink,userId)
values('New type of engines','pictureLink',2)
insert into Info(id,language,date,text,videoLink)
values(8,'English','11.11.2017','some text','videoLink')



insert into Headings(name, description)
values('Travelling', 'All about travelling')

insert into Headings(name, description)
values('Sport', 'All about sport')

insert into Headings(name, description)
values('Home', 'All about home')

insert into Headings(name, description)
values('Cars', 'All about cars')

insert into Headings(name, description)
values('Cinema', 'All about cinema')

insert into Headings(name, description)
values('News', 'All about news')

insert into Headings(name, description)
values('Culture', 'All about culture')

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

