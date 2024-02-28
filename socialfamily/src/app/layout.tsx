"use client"
import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { UIProviders } from "./UIProvider";
import { store } from '@/store'
import { Provider } from 'react-redux'

// const inter = Inter({ subsets: ["latin"] });

// export const metadata: Metadata = {
//   title: "Create Next App",
//   description: "Generated by create next app",
// };

export default function RootLayout({children}: { children: React.ReactNode }) {
  return (
    <html lang="en" className='dark'>
      <body>
        <Provider store={store}>
        <UIProviders>
          {children}
        </UIProviders>
        </Provider>
      </body>
    </html>
  );
}