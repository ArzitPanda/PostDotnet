"use client";
import NavbarCustom from "@/app/Component/Ui/Navbar";
import { RootState } from "@/store";
import { fetchProfile } from "@/store/Profile/ProfileSlice";
import {
  Image,
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
  AvatarGroup,
  Avatar,
} from "@nextui-org/react";
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

const page: React.FC<any> = ({ params }) => {
  const profileId = params.id;
  const dispatch = useDispatch();
  const state = useSelector((state: RootState) => state.profile);

  console.log(state);

  useEffect(() => {
    const userId = localStorage.getItem("id");

    dispatch<any>(fetchProfile({ id: profileId, id2: userId }));
  }, []);

  const { isOpen, onOpen, onOpenChange } = useDisclosure();
  return (
    <div className="grid grid-cols-8 bg-zinc-950 overflow-hidden">
      <div className="col-span-8 lg:hidden fixed top-0 w-full z-10 bg-black">
        <NavbarCustom data={state.user} />
      </div>
      <div className="col-span-8 lg:col-span-5 bg-black grid grid-cols-3 md:grid-cols-5 lg:grid-cols-7 gap-x-1 gap-y-1  items-start overflow-y-scroll">
        <div className="tab col-span-3 md:col-span-5 lg:col-span-7 h-32 lg:h-56 flex bg-zinc-800"></div>

        {state.posts.map((i, j) => {
          return (
            <div className="col-span-1  aspect-square bg-blue-300 ">
              <Image
                className="w-full h-full object-fill rounded-none"
                src={i?.imgUrl}
                onClick={onOpen}
              />
            </div>
          );
        })}
      </div>
      <div className="lg:block hidden col-span-3 bg-zinc-800 fixed top-0 right-0 w-[37.5%] h-screen">
        <div className="flex flex-row items-center justify-center mt-24">
          <Avatar
            src={state.user?.photoUrl}
            size="lg"
            isBordered
            color="warning"
            className="w-40 h-40 rounded-full"
          />
          <Avatar
            src={state.user?.photoUrl}
            className="w-40 h-40 rounded-full"
            isBordered
            color="primary"
          />
        </div>
        <div className="text-lg text-white w-full text-center font-semibold">
          {state.RelationType}
        </div>
      </div>

      <Modal
        isOpen={isOpen}
        onOpenChange={onOpenChange}
        isKeyboardDismissDisabled={true}
      >
        <ModalContent>
          {(onClose) => (
            <>
              <ModalHeader className="flex flex-col gap-1">
                Modal Title
              </ModalHeader>
              <ModalBody>
                <Image
                  className="w-full h-full object-fill"
                  src="https://as1.ftcdn.net/v2/jpg/01/89/72/80/1000_F_189728056_bLt6DiWQsSr4aG7Iz45qGrwFPwvWhpG8.jpg"
                />
              </ModalBody>
              <ModalFooter>
                <Button color="danger" variant="light" onPress={onClose}>
                  Close
                </Button>
                <Button color="primary" onPress={onClose}>
                  Action
                </Button>
              </ModalFooter>
            </>
          )}
        </ModalContent>
      </Modal>
    </div>
  );
};

export default page;
