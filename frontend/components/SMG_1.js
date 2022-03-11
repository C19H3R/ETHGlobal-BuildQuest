import React, { useRef } from 'react'
import { useGLTF } from '@react-three/drei'
import { useFrame } from "react-three-fiber"

export default function Model({ ...props }) {
  const group = useRef()
  const { nodes, materials } = useGLTF('/SMG_1.gltf')

  useFrame((state) => {
    const t = state.clock.getElapsedTime()
    //group.current.rotation.z = - 0.01 - (1 + Math.sin(t / 1.5)) / 10
    group.current.rotation.x = Math.cos(t / 4) / 5
    group.current.rotation.y = Math.sin(t / 2) / 1
    //group.current.position.y = (1 + Math.sin(t / 1.5)) / 20
  })

  return (
    <group ref={group} {...props} dispose={null}>
      <mesh geometry={nodes.Magazine_SMG.geometry} material={nodes.Magazine_SMG.material} position={[0.48, 0.15, 0]}/>
      <mesh geometry={nodes.Cube084.geometry} material={nodes.Cube084.material} />
      <mesh geometry={nodes.Cube084_1.geometry} material={materials.Grey} />
      <mesh geometry={nodes.Cube084_2.geometry} material={materials.White} />
      <mesh geometry={nodes.Cube084_3.geometry} material={materials.Main} />
    </group>
  )
}

useGLTF.preload('/SMG_1.gltf')
