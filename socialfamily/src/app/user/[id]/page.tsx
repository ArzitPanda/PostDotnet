'use client'
import NavbarCustom from '@/app/Component/Ui/Navbar'
import { Image,Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Button, useDisclosure } from '@nextui-org/react'
import React from 'react'

const page = () => {
  const {isOpen, onOpen, onOpenChange} = useDisclosure();
  return (
    <div className='grid grid-cols-8 bg-zinc-950 overflow-hidden'>
        <div className='col-span-8 lg:hidden fixed top-0 w-full z-10 bg-black'>
          <NavbarCustom/>
        </div>
        <div className='col-span-8 lg:col-span-5 bg-black grid grid-cols-3 md:grid-cols-5 lg:grid-cols-7 gap-x-1 gap-y-1  items-start overflow-y-scroll'>
        <div className='tab col-span-3 md:col-span-5 lg:col-span-7 h-32 lg:h-56 flex bg-blue-800'>

        </div>




          {new Array(100).fill(0).map((i, j) =>{return (<div className='col-span-1  aspect-square bg-blue-300 '>

          <Image className='w-full h-full object-fill' src='https://as1.ftcdn.net/v2/jpg/01/89/72/80/1000_F_189728056_bLt6DiWQsSr4aG7Iz45qGrwFPwvWhpG8.jpg' onClick={onOpen}/>

          </div>)})}
        </div>
        <div className='lg:block hidden col-span-3 bg-zinc-800 fixed top-0 right-0 w-[37.5%] h-screen'>
     
        </div> 

        <Modal isOpen={isOpen} onOpenChange={onOpenChange}  isKeyboardDismissDisabled={true}>
        <ModalContent>
          {(onClose) => (
            <>
              <ModalHeader className="flex flex-col gap-1">Modal Title</ModalHeader>
              <ModalBody>
              <Image className='w-full h-full object-fill' src='https://as1.ftcdn.net/v2/jpg/01/89/72/80/1000_F_189728056_bLt6DiWQsSr4aG7Iz45qGrwFPwvWhpG8.jpg' />

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
  )
}

export default page