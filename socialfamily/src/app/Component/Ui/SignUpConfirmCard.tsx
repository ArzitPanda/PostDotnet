import { Card, CardBody, CardFooter, CardHeader, Divider, Image, Link } from '@nextui-org/react'
import React from 'react'

const SignUpConfirmCard:React.FC<any> = ({useremail}) => {
  return (
    <Card className="w-[400px] my-6 bg-gray-900">
    <CardHeader className="flex gap-3">
      <Image
        alt="nextui logo"
        height={40}
        radius="sm"
        className='invert'
        src="brand.png"
        width={40}
      />
      <div className="flex flex-col">
        <p className="text-md">SocialTree</p>
        <p className="text-small text-default-500">Confirmation</p>
      </div>
    </CardHeader>
    <Divider/>
    <CardBody>
      <p>a email is sent to your registered email {useremail}</p>
    </CardBody>
    <Divider/>
    <CardFooter>
      <Link
        isExternal
        showAnchorIcon
        href="mailto:"
      >
        Visit to your mail.
      </Link>
    </CardFooter>
  </Card>
  )
}

export default SignUpConfirmCard