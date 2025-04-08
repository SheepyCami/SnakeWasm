window.getBoundingClientRect = (element) => {
    const rect = element.getBoundingClientRect();
    return {
        left: rect.left,
        top: rect.top,
        width: rect.width,
        height: rect.height
    };
};

window.setElementTransform = (element, transform) => {
    element.style.transform = transform;
}; 