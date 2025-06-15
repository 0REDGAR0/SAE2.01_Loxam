/*==============================================================*/
/* Nom de SGBD :  PostgreSQL 8                                  */
/* Date de création :  05/06/2025 11:59:02                      */
/*==============================================================*/


drop table  IF EXISTS  CATEGORIE CASCADE;

drop table  IF EXISTS  CERTIFICATION CASCADE;

drop table  IF EXISTS  CLIENT CASCADE;

drop table  IF EXISTS  DISPOSE CASCADE;

drop table  IF EXISTS  EMPLOYE CASCADE;

drop table  IF EXISTS  ETAT CASCADE;

drop table  IF EXISTS  MATERIEL CASCADE;

drop table  IF EXISTS  NECESSITER CASCADE;

drop table  IF EXISTS  NOMROLE CASCADE;

drop table  IF EXISTS  RESERVATION CASCADE;

drop table  IF EXISTS  TYPE CASCADE;

/*==============================================================*/
/* Table : CATEGORIE                                            */
/*==============================================================*/
create table CATEGORIE (
   NUMCATEGORIE         SERIAL               not null,
   LIBELLECATEGORIE     VARCHAR(30)          null,
   constraint PK_CATEGORIE primary key (NUMCATEGORIE)
);

/*==============================================================*/
/* Table : CERTIFICATION                                        */
/*==============================================================*/
create table CERTIFICATION (
   NUMCERTIFICATION     SERIAL               not null,
   LIBELLECERTIF        VARCHAR(30)          null,
   constraint PK_CERTIFICATION primary key (NUMCERTIFICATION)
);

/*==============================================================*/
/* Table : CLIENT                                               */
/*==============================================================*/
create table CLIENT (
   NUMCLIENT            SERIAL               not null,
   NOMCLIENT            VARCHAR(30)          null,
   PRENOMCLIENT         VARCHAR(30)          null,
   ADDRESSECLIENT       VARCHAR(120)         null,
   MAILCLIENT           VARCHAR(120)         null,
   NUMEROTELCLIENT      VARCHAR(12)          null,
   constraint PK_CLIENT primary key (NUMCLIENT)
);

/*==============================================================*/
/* Table : DISPOSE                                              */
/*==============================================================*/
create table DISPOSE (
   NUMCLIENT            INT4                 not null,
   NUMCERTIFICATION     INT4                 not null,
   constraint PK_DISPOSE primary key (NUMCLIENT, NUMCERTIFICATION)
);

/*==============================================================*/
/* Table : EMPLOYE                                              */
/*==============================================================*/
create table EMPLOYE (
   NUMEMPLOYE           SERIAL               not null,
   NUMROLE              INT4                 not null,
   NOM                  VARCHAR(30)          null,
   PRENOM               VARCHAR(30)          null,
   LOGIN                VARCHAR(30)          null,
   MDP                  VARCHAR(30)          null,
   constraint PK_EMPLOYE primary key (NUMEMPLOYE)
);

/*==============================================================*/
/* Table : ETAT                                                 */
/*==============================================================*/
create table ETAT (
   NUMETAT              SERIAL               not null,
   LIBELLEETAT          VARCHAR(30)          null,
   constraint PK_ETAT primary key (NUMETAT)
);

/*==============================================================*/
/* Table : MATERIEL                                             */
/*==============================================================*/
create table MATERIEL (
   NUMMATERIEL          SERIAL               not null,
   NUMETAT              INT4                 not null,
   NUMTYPE              INT4                 not null,
   REFERENCE            VARCHAR(50)          null,
   NOMMATERIEL          VARCHAR(100)         null,
   DESCRIPTIF           VARCHAR(300)         null,
   PRIXJOURNEE          DECIMAL(6,2)         null,
   constraint PK_MATERIEL primary key (NUMMATERIEL)
);

/*==============================================================*/
/* Table : NECESSITER                                           */
/*==============================================================*/
create table NECESSITER (
   NUMCERTIFICATION     INT4                 not null,
   NUMMATERIEL          INT4                 not null,
   constraint PK_NECESSITER primary key (NUMCERTIFICATION, NUMMATERIEL)
);

/*==============================================================*/
/* Table : NOMROLE                                              */
/*==============================================================*/
create table NOMROLE (
   NUMROLE              SERIAL               not null,
   NUMEMPLOYE           INT4                 null,
   NOMROLE              VARCHAR(30)          null,
   constraint PK_NOMROLE primary key (NUMROLE)
);

/*==============================================================*/
/* Table : RESERVATION                                          */
/*==============================================================*/
create table RESERVATION (
   NUMRESERVATION       SERIAL               not null,
   NUMMATERIEL          INT4                 not null,
   NUMEMPLOYE           INT4                 not null,
   NUMCLIENT            INT4                 not null,
   DATERESERVATION      DATE                 null,
   DATEDEBUTLOCATION    DATE                 null,
   DATERETOUREFFECTIVELOCATION DATE                 null,
   DATERETOURREELLELOCATION DATE                 null,
   PRIXTOTAL            DECIMAL(6,2)         null,
   constraint PK_RESERVATION primary key (NUMRESERVATION)
);

/*==============================================================*/
/* Table : TYPE                                                 */
/*==============================================================*/
create table TYPE (
   NUMTYPE              SERIAL               not null,
   NUMCATEGORIE         INT4                 not null,
   LIBELLETYPE          VARCHAR(30)          null,
   constraint PK_TYPE primary key (NUMTYPE)
);

alter table DISPOSE
   add constraint FK_DISPOSE_DISPOSE_CLIENT foreign key (NUMCLIENT)
      references CLIENT (NUMCLIENT)
      on delete restrict on update restrict;

alter table DISPOSE
   add constraint FK_DISPOSE_DISPOSE2_CERTIFIC foreign key (NUMCERTIFICATION)
      references CERTIFICATION (NUMCERTIFICATION)
      on delete restrict on update restrict;

alter table EMPLOYE
   add constraint FK_EMPLOYE_LIE2_NOMROLE foreign key (NUMROLE)
      references NOMROLE (NUMROLE)
      on delete restrict on update restrict;

alter table MATERIEL
   add constraint FK_MATERIEL_APPARTENI_TYPE foreign key (NUMTYPE)
      references TYPE (NUMTYPE)
      on delete restrict on update restrict;

alter table MATERIEL
   add constraint FK_MATERIEL_POSSEDE_ETAT foreign key (NUMETAT)
      references ETAT (NUMETAT)
      on delete restrict on update restrict;

alter table NECESSITER
   add constraint FK_NECESSIT_NECESSITE_CERTIFIC foreign key (NUMCERTIFICATION)
      references CERTIFICATION (NUMCERTIFICATION)
      on delete restrict on update restrict;

alter table NECESSITER
   add constraint FK_NECESSIT_NECESSITE_MATERIEL foreign key (NUMMATERIEL)
      references MATERIEL (NUMMATERIEL)
      on delete restrict on update restrict;

alter table NOMROLE
   add constraint FK_NOMROLE_LIE_EMPLOYE foreign key (NUMEMPLOYE)
      references EMPLOYE (NUMEMPLOYE)
      on delete restrict on update restrict;

alter table RESERVATION
   add constraint FK_RESERVAT_EFFECTUER_EMPLOYE foreign key (NUMEMPLOYE)
      references EMPLOYE (NUMEMPLOYE)
      on delete restrict on update restrict;

alter table RESERVATION
   add constraint FK_RESERVAT_PORTER_MATERIEL foreign key (NUMMATERIEL)
      references MATERIEL (NUMMATERIEL)
      on delete restrict on update restrict;

alter table RESERVATION
   add constraint FK_RESERVAT_REALISER_CLIENT foreign key (NUMCLIENT)
      references CLIENT (NUMCLIENT)
      on delete restrict on update restrict;

alter table TYPE
   add constraint FK_TYPE_DETAILLER_CATEGORI foreign key (NUMCATEGORIE)
      references CATEGORIE (NUMCATEGORIE)
      on delete restrict on update restrict;

