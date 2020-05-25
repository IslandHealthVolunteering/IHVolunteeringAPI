CREATE TABLE public."LoginUser"("Email" character varying(100) COLLATE pg_catalog."default" NOT NULL, "Password" character varying(1000) COLLATE pg_catalog."default" NOT NULL, "Secret" character varying COLLATE pg_catalog."default", CONSTRAINT "LoginUser_pkey" PRIMARY KEY ("Email"));
CREATE TABLE public."User"("Name" character varying(50) COLLATE pg_catalog."default" NOT NULL, "VolunteerHours" integer, "Email" character varying(100) COLLATE pg_catalog."default" NOT NULL, CONSTRAINT "User_pkey" PRIMARY KEY ("Email"), CONSTRAINT "Email" FOREIGN KEY ("Email") REFERENCES public."LoginUser" ("Email") MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION NOT VALID);
