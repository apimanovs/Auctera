<template>
  <svg
    xmlns="http://www.w3.org/2000/svg"
    :width="size"
    :height="size"
    :viewBox="viewBox"
    fill="none"
    :stroke="color"
    :stroke-width="strokeWidth"
    stroke-linecap="round"
    stroke-linejoin="round"
    :style="getStyle"
  >
    <g fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"><circle cx="12" cy="8" r="5"/><path d="M20 21a8 8 0 0 0-16 0"/></g>
  </svg>
</template>

<script>
export default {
  name: 'ProfileIcon',
  props: {
    size: { type: Number, default: 24 },
    color: { type: String, default: '#000000' },
    strokeWidth: { type: Number, default: 2 },
    background: { type: String, default: 'transparent' },
    opacity: { type: Number, default: 1 },
    rotation: { type: Number, default: 0 },
    shadow: { type: Number, default: 0 },
    flipHorizontal: { type: Boolean, default: false },
    flipVertical: { type: Boolean, default: false },
    padding: { type: Number, default: 0 }
  },
  computed: {
    viewBox() {
      const viewBoxSize = 24 + (this.padding * 2);
      const viewBoxOffset = -this.padding;
      return `${viewBoxOffset} ${viewBoxOffset} ${viewBoxSize} ${viewBoxSize}`;
    },
    getStyle() {
      const transforms = [];
      if (this.rotation !== 0) transforms.push(`rotate(${this.rotation}deg)`);
      if (this.flipHorizontal) transforms.push('scaleX(-1)');
      if (this.flipVertical) transforms.push('scaleY(-1)');

      return {
        opacity: this.opacity,
        transform: transforms.join(' ') || undefined,
        filter: this.shadow > 0 ? `drop-shadow(0 ${this.shadow}px ${this.shadow * 2}px rgba(0,0,0,0.3))` : undefined,
        backgroundColor: this.background !== 'transparent' ? this.background : undefined
      };
    }
  }
}
</script>